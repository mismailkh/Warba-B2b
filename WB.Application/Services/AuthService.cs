using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WB.Domain.Common;
using WB.Domain.Entities.Lookups;
using WB.Domain.Entities.Ums;
using WB.Application.Interfaces.Repositories;
using WB.Application.Interfaces.Services;
using WB.Shared.Dtos;
using WB.Shared.Dtos.General.RequestDtos;
using WB.Shared.Dtos.General.ResponseDtos;
using WB.Shared.Dtos.UMS.Others;
using WB.Shared.Dtos.UMS.RequestDtos;
using WB.Shared.Dtos.UMS.ResponseDtos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static WB.Shared.Enums.UmsEnums;
using Claim = System.Security.Claims.Claim;
using ClaimUms = WB.Domain.Entities.Ums.Claim;

namespace WB.Application.Services
{
    public class AuthService(JwtSettings _jwtSettings, IUserRepository _userInfoRepository, IOrganizationRepository _organizationRepository, IGenericRepository<User> _userRepository,
            IGenericRepository<GroupClaims> _userGroupClaimRepository, UserManager<User> _userManager, RoleManager<Role> _roleManager,
            IMapper _mapper, IGenericRepository<Translation> _translationRepository, IGenericRepository<ClaimUms> _claimRepository,
            IAuthRepository _iAuthRepository, IGenericRepository<UserRoles> _userRoleRepository, IEmailService _emailService, IConfiguration _config)
         : IAuthService
    {
        public async Task<AuthenticationResult> Login(string email, string password, string culture, bool hasTranslations)
        {
            try
            {
                string errorCode = string.Empty;
                var user = await _userManager.FindByEmailAsync(email);
                if (user != null && !user.IsDeleted && !user.IsLocked && user.IsActive && user.EmailConfirmed)
                {
                    var userHasValidPassword = await _userManager.CheckPasswordAsync(user, password);
                    int loginFailedCount = await _userManager.GetAccessFailedCountAsync(user);
                    if (!userHasValidPassword)
                    {
                        if (_userManager.SupportsUserLockout)
                        {
                            await _userManager.AccessFailedAsync(user);
                            if (loginFailedCount >= 2)
                            {
                                user.IsLocked = true;
                                _userRepository.Update(user);
                                await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.UtcNow.AddMinutes(59));
                                return new AuthenticationResult
                                {
                                    ErrorCode = Enum.GetName(LoginErrorEnum.UserLockedDueToInvalidAttempts)
                                };
                            }
                        }
                        errorCode = Enum.GetName(LoginErrorEnum.UserEmailOrPasswordIncorrect);
                    }
                    else
                    {
                        if (_userManager.SupportsUserLockout && loginFailedCount > 0)
                        {
                            await _userManager.ResetAccessFailedCountAsync(user);
                            await _userManager.SetLockoutEndDateAsync(user, null);
                        }
                        return await GenerateAuthenticationResult(user, hasTranslations, culture);
                    }
                }
                else if (user != null)
                {
                    if (!user.IsActive)
                    {
                        errorCode = Enum.GetName(LoginErrorEnum.UserIsDeactivated);
                    }
                    else if (user.IsLocked)
                    {
                        if (user.LockoutEnd <= DateTime.UtcNow)
                        {
                            await _userManager.ResetAccessFailedCountAsync(user);
                            await _userManager.SetLockoutEndDateAsync(user, null);
                            await Login(email, password, culture, hasTranslations);
                        }
                        else
                        {
                            errorCode = Enum.GetName(LoginErrorEnum.UserLockedDueToInvalidAttempts);
                        }
                    }
                    else if (!user.EmailConfirmed)
                    {
                        errorCode = Enum.GetName(LoginErrorEnum.UserEmailNotConfirmed);
                    }
                    else
                    {
                        errorCode = Enum.GetName(LoginErrorEnum.UserIsInactiveOrDeleted);
                    }
                }
                else
                {
                    errorCode = Enum.GetName(LoginErrorEnum.UserDoesNotExist);
                }
                return new AuthenticationResult { ErrorCode = errorCode };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private async Task<AuthenticationResult> GenerateAuthenticationResult(User user, bool hasTranslations, string culture)
        {
            try
            {
                List<TranslationResponseDto> translations = new List<TranslationResponseDto>();
                var userRoles = (await _userRoleRepository.Get(x => x.UserId == user.Id)).Select(x => x.RoleId).ToList();
                List<ClaimSucessResponse> claimList = new List<ClaimSucessResponse>();
                // Get User Specific Claims
                //await GetUserClaims(user, claimList);
                // Get Group Specific Claims
                var userDetail = (await _userRepository.Get(x => x.Id == user.Id)).FirstOrDefault();
                
                // Get User specific Role Claims
                //var roleClaims = await _iAuthRepository.GetUserRoleClaims(userRoles);
                //if (roleClaims.Count() > 0)
                //{
                //    foreach (var item in roleClaims)
                //    {
                //        claimList.Add(item);
                //    }
                //}

                IList<Claim> EmptyClaims = new List<Claim>();
                EmptyClaims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Email));
                EmptyClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                EmptyClaims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
                EmptyClaims.Add(new Claim(JwtRegisteredClaimNames.Name, user.UserName));
                EmptyClaims.Add(new Claim("Id", user.Id));

                var tokenhandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
                var tokenDescription = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(EmptyClaims),
                    Expires = DateTime.UtcNow.Add(_jwtSettings.TokenLifeTime),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature)
                };
                var writeToken = tokenhandler.WriteToken(tokenhandler.CreateToken(tokenDescription));
                Guid tokenId = Guid.NewGuid();
                if (hasTranslations == false)
                {
                    var transla = await _translationRepository.Get();
                    translations = _mapper.Map<List<TranslationResponseDto>>(transla);
                }

                userDetail.PersonalInformation = await _userInfoRepository.GetUserPersonalInformation(userDetail.Id);
                var departmentInformation = await _organizationRepository.GetDepartmentDetail(userDetail.PersonalInformation.DepartmentId.Value, culture);
                var authenticationResult = new AuthenticationResult
                {
                    Success = true,
                    Token = writeToken,
                    RefreshToken = tokenId.ToString(),
                    ClaimsResultList = claimList,
                    Translations = translations,
                    ProfilePicUrl = userDetail?.PersonalInformation?.Avatar,
                    UserDetail = new UserDetailLoginResponseDto
                    {
                        UserId = user.Id,
                        UserName = user.UserName,
                        SecurityStamp = user.SecurityStamp,
                        Email = user.Email,
                        UserRoles = userRoles,
                        FullNameEn = userDetail?.PersonalInformation?.FirstNameEn + " " + userDetail?.PersonalInformation?.LastNameEn,
                        FullNameAr = userDetail?.PersonalInformation?.FirstNameAr + " " + userDetail?.PersonalInformation?.LastNameAr,
                        CivilId = userDetail?.PersonalInformation?.CivilId,
                        IsPasswordReset = userDetail.IsPasswordReset,
                        OrganizationId = departmentInformation.OrganizationId,
                        DepartmentId = userDetail.PersonalInformation?.DepartmentId,
                        DepartmentName = culture == "en-US" ? departmentInformation.NameEn : departmentInformation.NameAr,
                    }
                };
                return authenticationResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private async Task GetUserClaims(User user, List<ClaimSucessResponse> claimList)
        {
            var userclaimsList = await _userManager.GetClaimsAsync(user);
            foreach (var item in userclaimsList)
            {
                ClaimSucessResponse claimsobj = new ClaimSucessResponse();
                claimsobj.Type = item.Type;
                claimsobj.Value = item.Value;
                claimList.Add(claimsobj);
            }
        }
        private async Task GetGroupClaims(List<UserGroup> userGroups, IList<ClaimSucessResponse> claimList)
        {
            try
            {
                foreach (var userGroup in userGroups)
                {
                    IList<GroupClaims> claimsList = new List<GroupClaims>();
                    claimsList = (await _userGroupClaimRepository.Get(x => x.GroupId == userGroup.GroupId)).ToList();
                    foreach (var item in claimsList)
                    {
                        var claim = await _claimRepository.GetById(item.Id);
                        ClaimSucessResponse claimsobj = new ClaimSucessResponse();
                        claimsobj.Type = claim.ClaimType;
                        claimsobj.Value = claim.ClaimValue;
                        claimList.Add(claimsobj);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<List<ClaimListResponseDto>> GetClaimsList(string culture)
        {
            try
            {
                var claims = await _claimRepository.Get(x => !x.IsDeleted);
                return _mapper.Map<List<ClaimListResponseDto>>(claims, opts =>
                {
                    opts.Items["culture"] = culture;
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<List<ClaimSucessResponse>> GetUserAssignedClaims(string userId)
        {
            try
            {
                var claims = await _iAuthRepository.GetUserAssignedClaims(userId);
                return _mapper.Map<List<ClaimSucessResponse>>(claims);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<UserClaimsRolesResponseDto> GetUserLatestClaimAndRoles(string userId)
        {
            try
            {
                var claims = await _iAuthRepository.GetUserLatestClaimAndRoles(userId);
                return _mapper.Map<UserClaimsRolesResponseDto>(claims);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<SecurityStampResponseDto> GetSecurityStampByEmail(string userId)
        {
            try
            {
                return await _iAuthRepository.GetSecurityStampByEmail(userId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task AdminChangeUserPassword(AdminChangeUserPasswordRequestDto userPasswordRequest)
        {
            try
            {
                await _iAuthRepository.AdminChangeUserPassword(userPasswordRequest);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #region Find User By Email
        public async Task<bool> FindUserByEmail(string email)
        {
            var response = await _userManager.FindByEmailAsync(email);
            if (response == null)
            {
                return false;
            }
            return true;
        }
        #endregion

        #region Find User By UserName
        public async Task<bool> FindUserByUserName(string username)
        {
            var response = await _userManager.FindByNameAsync(username);
            if (response == null)
            {
                return false;
            }
            return true;
        }
        #endregion

        #region Send Email Confirmation Link

        public async Task SendEmailConfirmationLink(string email)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user != null)
                {
                    var confirmEmailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    string url = $"{_config["webUrl"]}confirm-email/{user.Id}/{WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(confirmEmailToken))}";
                    EmailRequestDto emailRequestDto = new EmailRequestDto
                    {
                        To = user.Email,
                        Subject = "Confirm Email Account",
                        Message = File.ReadAllText(Directory.GetCurrentDirectory() + "\\EmailTemplates\\EmailConfirmation.html").Replace("#emailConfirmationLink", url)
                    };
                    await _emailService.SendEmail(emailRequestDto);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Confirm Email

        public async Task<IdentityResult> ConfirmEmail(string userId, string confrimationToken)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    return await _userManager.ConfirmEmailAsync(user, Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(confrimationToken)));
                }
                else
                {
                    return new IdentityResult();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Send Password Reset Link

        public async Task SendPasswordResetLink(string email)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user != null)
                {
                    var resetPasswordToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var url = $"{_config["webUrl"]}reset-password/{user.Id}/{WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(resetPasswordToken))}";
                    EmailRequestDto emailRequestDto = new EmailRequestDto
                    {
                        To = user.Email,
                        Subject = "Reset Password",
                        Message = File.ReadAllText(Directory.GetCurrentDirectory() + "\\EmailTemplates\\ResetPassword.html").Replace("#resetPasswordLink", url)
                    };

                    await _emailService.SendEmail(emailRequestDto);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Reset Password

        public async Task<IdentityResult> ResetPassword(ResetPasswordRequestDto resetPasswordRequest)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(resetPasswordRequest.UserId);
                if (user != null)
                {
                    if (!user.EmailConfirmed)
                    {
                        await _userManager.ConfirmEmailAsync(user, await _userManager.GenerateEmailConfirmationTokenAsync(user));
                    }
                    return await _userManager.ResetPasswordAsync(user, Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(resetPasswordRequest.ResetPasswordToken)), resetPasswordRequest.Password);
                }
                else
                {
                    return new IdentityResult();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
