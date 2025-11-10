using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WB.Application.Interfaces.Repositories;
using WB.Domain.Entities.Ums;
using WB.Infrastructure.DbContext;
using WB.Shared.Dtos;
using WB.Shared.Dtos.General.ResponseDtos;
using WB.Shared.Dtos.UMS.RequestDtos;
using WB.Shared.Dtos.UMS.ResponseDtos;

namespace WB.Infrastructure.Repository
{
    public class AuthRepository(DatabaseContext _dbContext, UserManager<User> _userManager, IMapper _mapper) : IAuthRepository
    {
        public async Task AdminChangeUserPassword(AdminChangeUserPasswordRequestDto userPasswordRequest)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    var user = await _userManager.FindByIdAsync(userPasswordRequest.UserId);
                    if (user != null)
                    {
                        string passwordChangeToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                        var changeResult = await _userManager.ResetPasswordAsync(user, passwordChangeToken, userPasswordRequest.Password);
                        if (changeResult.Succeeded)
                        {
                            user.IsPasswordReset = false;
                            _dbContext.Users.Update(user);

                            UserPasswordHistory passwordHistory = new UserPasswordHistory
                            {
                                HistoryId = Guid.NewGuid(),
                                UserId = user.Id,
                                CreatedBy = userPasswordRequest.CreatedBy
                            };
                            //_dbContext.UserPasswordHistory.Add(passwordHistory);

                            var identityuser = await _userManager.FindByEmailAsync(user.Email);
                            await _userManager.UpdateSecurityStampAsync(identityuser);
                            await _dbContext.SaveChangesAsync();
                            transaction.Commit();
                        }
                    }
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }
        public async Task<SecurityStampResponseDto> GetSecurityStampByEmail(string email)
        {
            try
            {
                var res = await _dbContext.Users.Where(x => x.Email == email).Select(x => x.SecurityStamp).FirstOrDefaultAsync();
                return new SecurityStampResponseDto { SecurityStamp = res };
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<ClaimSucessResponse>> GetUserAssignedClaims(string userId)
        {
            try
            {
                //var userClaims = await _dbContext.UserClaims
                //        .Where(uc => uc.UserId == userId)
                //        .Select(uc => new ClaimSucessResponse
                //        {
                //            Type = uc.ClaimType,
                //            Value = uc.ClaimValue
                //        })
                //        .ToListAsync();

                //var userGroupIds = await _dbContext.UserGroup.Include(gr=>gr.Group)
                //    .Where(ug => ug.UserId == userId && !ug.Group.IsDeleted)
                //    .Select(ug => ug.GroupId)
                //    .ToListAsync();

                //var groupClaims = new List<ClaimSucessResponse>();

                //if (userGroupIds.Any())
                //{
                //    groupClaims = await _dbContext.GroupClaims
                //        .Where(gc => userGroupIds.Contains(gc.GroupId))
                //        .Include(gc => gc.Claim)
                //        .Select(gc => new ClaimSucessResponse
                //        {
                //            Type = gc.Claim.ClaimType,
                //            Value = gc.Claim.ClaimValue
                //        })
                //        .ToListAsync();
                //}

                //var allClaims = userClaims.Concat(groupClaims).ToList();
                return new List<ClaimSucessResponse>();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<UserClaimsRolesResponseDto> GetUserLatestClaimAndRoles(string userId)
        {
            try
            {
                var claims = await GetUserAssignedClaims(userId);
                var userActiveRoles = await _dbContext.UserRoles.Include(ur => ur.Role)
                    .Where(ur => ur.UserId == userId && ur.Role.IsActive)
                    .Select(ur => ur.Role.Id).ToListAsync();

                return new UserClaimsRolesResponseDto
                {
                    Claims = claims,
                    Roles = userActiveRoles.Select(roleId => new UserAssignedRoleResponseDto { RoleId = roleId }).ToList()
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
