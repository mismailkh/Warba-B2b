using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;
using WB.Application.Interfaces.Repositories;
using WB.Application.Interfaces.Services;
using WB.Domain.Entities.Ums;
using WB.Shared.Configs;
using WB.Shared.Dtos.General.RequestDtos;
using WB.Shared.Dtos.UMS.RequestDtos;
using WB.Shared.Dtos.UMS.ResponseDtos;
using WB.Shared.Enums;
using static WB.Shared.Enums.GeneralEnums;
using static WB.Shared.Enums.NotificationEnums;

namespace WB.Application.Services
{
    public class UserService(IUserRepository _userRepository, ILoggingService _iLoggingService, IEmailService _emailService, IConfiguration _config, IHttpContextAccessor _httpContext, INotificationService _notificationService, IMapper _imapper, UserManager<User> _userManager) : IUserService
    {
        public async Task<List<UserListResponseDto>> GetUsersList(string culture)
        {
            try
            {
                return await _userRepository.GetUsersList(culture);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<UserDetailResponseDto> GetUserDetail(string culture, string userId)
        {
            try
            {
                return await _userRepository.GetUserDetail(culture, userId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<List<ProcessLogsResponseDto>> GetProcessLogsByUserId(string userId)
        {
            try
            {
                return await _userRepository.GetProcessLogsByUserId(userId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task CreateAdminUser(CreateAdminRequestDto adminRequest)
        {
            try
            {
                await _userRepository.CreateAdminUser(adminRequest);
                await _iLoggingService.CreateProcessLog(new SaveProcessLogRequestDto
                {
                    Module = ModuleEnum.Ums.GetDisplayName(),
                    Process = "Create_Admin_User",
                    Description = "Admin_User_Created",
                    IPAddress = _httpContext.HttpContext?.Connection?.RemoteIpAddress?.ToString(),
                    LogDate = DateTime.Now,
                });
            }
            catch (Exception ex)
            {
                await _iLoggingService.SaveErrorLogDetails(new SaveErrorLogRequestDto
                {
                    Module = ModuleEnum.Ums.GetDisplayName(),
                    Operation = "Create_Admin_User",
                    Message = ex.Message,
                    StackTrace = ex.StackTrace,
                    Source = ex.Source,
                    IPAddress = _httpContext.HttpContext?.Connection?.RemoteIpAddress?.ToString(),
                    LogDate = DateTime.Now,
                    Token = _httpContext.HttpContext?.Request?.Headers["Authorization"].ToString()?.Replace("Bearer ", "")
                });
                throw;
            }
        }
        public async Task SaveUser(SaveUserRequestDto userRequest)
        {
            try
            {
                bool sendConfirmationEmail = userRequest.Id == null;
                userRequest.Password = await GenerateRandomPassword();
                var newUserResponse = await _userRepository.SaveUser(userRequest);
                if (newUserResponse.Succeeded)
                {
                    if (sendConfirmationEmail && newUserResponse.User != null)
                    {
                        await SendConfirmationEmail(userRequest, newUserResponse.User);
                        newUserResponse.User = null;
                    }
                    await _iLoggingService.CreateProcessLog(new SaveProcessLogRequestDto
                    {
                        Module = ModuleEnum.Ums.GetDisplayName(),
                        Process = userRequest.Id == null ? "Add_New_User" : "Update_User",
                        Description = userRequest.Id == null ? "New_User_Created" : "User_Updated",
                        IPAddress = _httpContext.HttpContext?.Connection?.RemoteIpAddress?.ToString(),
                        LogDate = DateTime.Now,
                        Token = _httpContext.HttpContext?.Request?.Headers["Authorization"].ToString()?.Replace("Bearer ", "")
                    });
                    // send browser notification
                    //if (userRequest.Roles.RoleId == SystemRoles.SystemAdmin)
                    //{
                    //    List<SendNotificationRequestDto> bulkNotificationRequests = new List<SendNotificationRequestDto>();
                    //    bulkNotificationRequests.Add(new SendNotificationRequestDto
                    //    {
                    //        EntityName = userRequest.PersonalInformation.FullName,
                    //        EntityId = userRequest.Id.ToString(),
                    //        EventId = (int)NotificationEventEnum.AddSiteUser,
                    //        Culture = userRequest.Culture,
                    //        SenderId = userRequest.CreatedBy,
                    //        Action = "user-added",
                    //        ModuleId = (int)ModuleEnum.SiteManagement,
                    //        ReceiverId = userRequest.Id,
                    //        SiteName = userRequest.SiteName
                    //    });
                    //    await _notificationService.SendNotification(bulkNotificationRequests);
                    //}
                }
            }
            catch (Exception ex)
            {
                await _iLoggingService.SaveErrorLogDetails(new SaveErrorLogRequestDto
                {
                    Module = ModuleEnum.Ums.GetDisplayName(),
                    Operation = userRequest.Id == null ? "Add_New_User" : "Update_User",
                    Message = ex.Message,
                    StackTrace = ex.StackTrace,
                    Source = ex.Source,
                    IPAddress = _httpContext.HttpContext?.Connection?.RemoteIpAddress?.ToString(),
                    LogDate = DateTime.Now,
                    Token = _httpContext.HttpContext?.Request?.Headers["Authorization"].ToString()?.Replace("Bearer ", "")
                });
                throw;
            }
        }
        private async Task SendConfirmationEmail(SaveUserRequestDto userRequest, dynamic newUser)
        {
            try
            {
                EmailRequestDto emailRequestDto = new EmailRequestDto();
                //if (userRequest.Roles.RoleId == SystemRoles.Patient)
                //{
                //    var confirmEmailToken = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                //    string url = $"{_config["webUrl"]}confirm-email/{newUser.Id}/{WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(confirmEmailToken))}";
                //    emailRequestDto = new EmailRequestDto
                //    {
                //        To = newUser.Email,
                //        Subject = "Confirm Email Account",
                //        Message = File.ReadAllText(Directory.GetCurrentDirectory() + "\\EmailTemplates\\EmailConfirmation.html").Replace("#emailConfirmationLink", url)
                //    };
                //}
                //else if (userRequest.Roles.RoleId == SystemRoles.SystemAdmin)
                //{
                    var resetPasswordToken = await _userManager.GeneratePasswordResetTokenAsync(newUser);
                    var url = $"{_config["webUrl"]}reset-password/{newUser.Id}/{WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(resetPasswordToken))}";
                    emailRequestDto = new EmailRequestDto
                    {
                        To = newUser.Email,
                        Subject = "New account at PsycReality - Action Required",
                        Message = File.ReadAllText(Directory.GetCurrentDirectory() + "\\EmailTemplates\\AccountSetup.html").Replace("#resetPasswordLink", url)
                    };
                //}
                await _emailService.SendEmail(emailRequestDto);
            }
            catch(Exception ex)
            {

            }
        }
        private async Task<string> GenerateRandomPassword()
        {
            try
            {
                int length = 12;
                string LowercaseChars = "abcdefghijklmnopqrstuvwxyz";
                string UppercaseChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                string DigitChars = "0123456789";
                string SpecialChars = "!@#$%^&*()_-+=<>?";

                var charPool = new StringBuilder();
                charPool.Append(LowercaseChars);
                charPool.Append(UppercaseChars);
                charPool.Append(DigitChars);
                charPool.Append(SpecialChars);

                var password = new char[length];
                var charPoolArray = charPool.ToString().ToCharArray();
                var bytes = new byte[length];

                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(bytes);
                }
                for (int i = 0; i < length; i++)
                {
                    password[i] = charPoolArray[bytes[i] % charPoolArray.Length];
                }
                string output = new string(password.OrderBy(x => Guid.NewGuid()).ToArray());
                var passwordValidator = new PasswordValidator<User>();
                var result = await new PasswordValidator<User>().ValidateAsync(_userManager, null, output);
                if (!result.Succeeded)
                {
                    return await GenerateRandomPassword();
                }
                else
                {
                    return output;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public async Task SaveUserAccess(SaveUserAccessRequestDto userAccessRequest)
        {
            try
            {
                await _userRepository.SaveUserAccess(userAccessRequest);
                await _iLoggingService.CreateProcessLog(new SaveProcessLogRequestDto
                {
                    Module = ModuleEnum.Ums.GetDisplayName(),
                    Process = "Save_User_Access",
                    Description = "User_Access_Updated",
                    IPAddress = _httpContext.HttpContext?.Connection?.RemoteIpAddress?.ToString(),
                    LogDate = DateTime.Now,
                    Token = _httpContext.HttpContext?.Request?.Headers["Authorization"].ToString()?.Replace("Bearer ", "")
                });
            }
            catch (Exception ex)
            {
                await _iLoggingService.SaveErrorLogDetails(new SaveErrorLogRequestDto
                {
                    Module = ModuleEnum.Ums.GetDisplayName(),
                    Operation = "Save_User_Access",
                    Message = ex.Message,
                    StackTrace = ex.StackTrace,
                    Source = ex.Source,
                    IPAddress = _httpContext.HttpContext?.Connection?.RemoteIpAddress?.ToString(),
                    LogDate = DateTime.Now,
                    Token = _httpContext.HttpContext?.Request?.Headers["Authorization"].ToString()?.Replace("Bearer ", "")
                });
                throw;
            }
        }
        public async Task UpdateUserStatus(UpdateUserStatusRequestDto userStatusRequest)
        {
            try
            {
                await _userRepository.UpdateUserStatus(userStatusRequest);
                await _iLoggingService.CreateProcessLog(new SaveProcessLogRequestDto
                {
                    Module = ModuleEnum.Ums.GetDisplayName(),
                    Process = "Update_User_Status",
                    Description = userStatusRequest.IsActive ? "User_Status_Marked_Active" : "User_Status_Marked_InActive",
                    IPAddress = _httpContext.HttpContext?.Connection?.RemoteIpAddress?.ToString(),
                    LogDate = DateTime.Now,
                    Token = _httpContext.HttpContext?.Request?.Headers["Authorization"].ToString()?.Replace("Bearer ", "")
                });
            }
            catch (Exception ex)
            {
                await _iLoggingService.SaveErrorLogDetails(new SaveErrorLogRequestDto
                {
                    Module = ModuleEnum.Ums.GetDisplayName(),
                    Operation = "Update_User_Status",
                    Message = ex.Message,
                    StackTrace = ex.StackTrace,
                    Source = ex.Source,
                    IPAddress = _httpContext.HttpContext?.Connection?.RemoteIpAddress?.ToString(),
                    LogDate = DateTime.Now,
                    Token = _httpContext.HttpContext?.Request?.Headers["Authorization"].ToString()?.Replace("Bearer ", "")
                });
                throw;
            }
        }

    }
}
