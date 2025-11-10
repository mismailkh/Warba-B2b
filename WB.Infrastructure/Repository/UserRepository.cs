using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WB.Application.Interfaces.Repositories;
using WB.Domain.Entities.Ums;
using WB.Infrastructure.DbContext;
using WB.Shared.Configs;
using WB.Shared.Dtos.UMS.RequestDtos;
using WB.Shared.Dtos.UMS.ResponseDtos;
using static WB.Shared.Enums.UmsEnums;

namespace WB.Infrastructure.Repository
{
    public class UserRepository(DatabaseContext _dbContext, UserManager<User> _userManager, IMapper _mapper) : IUserRepository
    {
        #region User List

        public async Task<List<UserListResponseDto>> GetUsersList(string culture)
        {
            try
            {
                return await _dbContext.UserListResponseDto.FromSql($"select * from ums.fUserList({culture})").AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region User Detail

        public async Task<UserDetailResponseDto> GetUserDetail(string culture, string userId)
        {
            try
            {
                var user = await _dbContext.Users
                    .Include(u => u.PersonalInformation)
                    .Include(u => u.PersonalInformation)
                    .Include(u => u.UserClaims)
                    .AsNoTracking().FirstOrDefaultAsync(u => u.Id == userId);

                return _mapper.Map<UserDetailResponseDto>(user, opts =>
                {
                    opts.Items["culture"] = culture;
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<UserPersonalInformation> GetUserPersonalInformation(string userId)
        {
            try
            {
                return await _dbContext.UserPersonalInformation.Where(u => u.UserId == userId).AsNoTracking().FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        #region Save User

        public async Task<SaveUserResponseDto> SaveUser(SaveUserRequestDto userRequest)
        {
            using (_dbContext)
            {
                using (var transaction = await _dbContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        User user = new();
                        if (userRequest.Id == null)
                        {
                            user = new User
                            {
                                Id = Guid.NewGuid().ToString(),
                                Email = userRequest.Email,
                                UserName = userRequest.UserName,
                                CreatedBy = userRequest.CreatedBy,
                                CreatedDate = DateTime.Now
                            };
                            var newUser = await _userManager.CreateAsync(user, userRequest.Password);
                            if (!newUser.Succeeded)
                            {
                                return new SaveUserResponseDto { ErrorCode = newUser.Errors.Any() ? newUser.Errors.First().Code : Enum.GetName(IdentityErrorEnum.DefaultError) };
                            }
                            userRequest.Id = user.Id;
                        }
                        else
                        {
                            user = await _dbContext.Users.FindAsync(userRequest.Id);
                        }
                        await SaveUserPersonalInformation(_dbContext, userRequest, user);
                        await _dbContext.SaveChangesAsync();
                        await transaction.CommitAsync();
                        return new SaveUserResponseDto { Succeeded = true, User = user };
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        throw;
                    }
                }
            }
        }

        private async Task SaveUserPersonalInformation(DatabaseContext dbContext, SaveUserRequestDto userRequest, User user)
        {
            try
            {
                var userPersonalInformation = dbContext.UserPersonalInformation.Where(x => x.UserId == user.Id).FirstOrDefault();
                if (userPersonalInformation != null)
                    dbContext.UserPersonalInformation.Remove(userPersonalInformation);
                userPersonalInformation = _mapper.Map<UserPersonalInformation>(userRequest.PersonalInformation);
                userPersonalInformation.UserId = user.Id;
                userPersonalInformation.CreatedBy = userRequest.CreatedBy;
                userPersonalInformation.CreatedDate = DateTime.Now;
                await dbContext.UserPersonalInformation.AddAsync(userPersonalInformation);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        #region User Access
        public async Task SaveUserAccess(SaveUserAccessRequestDto userAccessRequest)
        {
            using (_dbContext)
            {
                using (var transaction = await _dbContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        await SaveUserClaims(_dbContext, userAccessRequest);
                        await _dbContext.SaveChangesAsync();
                        await transaction.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        throw;
                    }
                }
            }
        }
        private async Task SaveUserClaims(DatabaseContext dbContext, SaveUserAccessRequestDto userAccessRequest)
        {
            try
            {
                var userClaims = dbContext.UserClaims.Where(x => x.UserId == userAccessRequest.UserId).ToList();
                if (userClaims != null)
                {
                    dbContext.UserClaims.RemoveRange(userClaims);
                }
                foreach (var claim in userAccessRequest.ClaimsList)
                {
                    UserClaims userClaim = new UserClaims
                    {
                        UserId = userAccessRequest.UserId,
                        ClaimType = claim.ClaimType,
                        ClaimValue = claim.ClaimValue,
                        CreatedBy = userAccessRequest.CreatedBy,
                        CreatedDate = DateTime.Now,
                    };
                    await dbContext.UserClaims.AddAsync(userClaim);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        #region User Activity
        public async Task<List<ProcessLogsResponseDto>> GetProcessLogsByUserId(string userId)
        {
            try
            {
                var userActivities = await _dbContext.ProcessLogs.Where(x => x.CreatedBy == userId).OrderByDescending(x => x.LogDate).AsNoTracking().ToListAsync();
                return _mapper.Map<List<ProcessLogsResponseDto>>(userActivities);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        #endregion

        #region Update User Status

        public async Task UpdateUserStatus(UpdateUserStatusRequestDto userStatusRequest)
        {
            using (_dbContext)
            {
                using (var transaction = await _dbContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        User user = await _dbContext.Users.FindAsync(userStatusRequest.UserId);
                        if (user != null)
                        {
                            user.IsActive = userStatusRequest.IsActive;
                            user.IsLocked = userStatusRequest.IsLocked;
                            user.ModifiedBy = userStatusRequest.ModifiedBy;
                            user.ModifiedDate = DateTime.Now;
                            await UpdateUserSecurityStamp(user.Email);
                            await _dbContext.SaveChangesAsync();
                            await transaction.CommitAsync();
                        }
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        throw;
                    }
                }
            }
        }
        #endregion

        #region Security Stamp Update

        public async Task UpdateUserSecurityStamp(string email)
        {
            try
            {
                var identityUser = await _userManager.FindByEmailAsync(email);
                var result = await _userManager.UpdateSecurityStampAsync(identityUser);
                if (!result.Succeeded)
                    throw new Exception();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        #region Get Employee Activities
        public Task<List<UserActivity>> GetEmployeeActivities(string userId)
        {
            try
            {
                return _dbContext.UserActivity.Where(x => x.UserId == userId).ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region Create Admin

        public async Task CreateAdminUser(CreateAdminRequestDto adminRequest)
        {
            using (_dbContext)
            {
                using (var transaction = await _dbContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        User user = new User
                        {
                            Id = Guid.NewGuid().ToString(),
                            Email = adminRequest.Email,
                            UserName = adminRequest.Email,
                            EmailConfirmed = true,
                            CreatedBy = "Self",
                            CreatedDate = DateTime.Now
                        };
                        var newUser = await _userManager.CreateAsync(user, adminRequest.Password);
                        if (!newUser.Succeeded)
                        {
                            throw new Exception(newUser.Errors.FirstOrDefault()?.Code, new Exception(newUser.Errors.FirstOrDefault()?.Description));
                        }

                        UserPersonalInformation userPersonalInformation = new UserPersonalInformation
                        {
                            UserId = user.Id,
                            FirstNameEn = adminRequest.FirstNameEn,
                            FirstNameAr = adminRequest.FirstNameAr,
                            LastNameEn = adminRequest.LastNameEn,
                            LastNameAr = adminRequest.LastNameAr,
                            CreatedBy = user.Id,
                            CreatedDate = DateTime.Now
                        };

                        await _dbContext.UserPersonalInformation.AddAsync(userPersonalInformation);

                        Role adminRole = new();
                        adminRole = await _dbContext.Roles.FindAsync(SystemRoles.SuperAdmin);
                        if (adminRole == null)
                        {
                            adminRole = new Role
                            {
                                Id = SystemRoles.SuperAdmin,
                                NameEn = "Super Admin",
                                NameAr = "Super Admin",
                                CreatedBy = user.Id,
                                CreatedDate = DateTime.Now,
                                IsActive = true
                            };
                            await _dbContext.Roles.AddAsync(adminRole);
                        }
                        UserRoles userRole = new UserRoles
                        {
                            UserId = user.Id,
                            RoleId = adminRole.Id,
                            CreatedBy = user.Id,
                            CreatedDate = DateTime.Now
                        };
                        await _dbContext.UserRoles.AddAsync(userRole);
                        await _dbContext.SaveChangesAsync();
                        await transaction.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        throw;
                    }
                }
            }
        }
        #endregion
    }
}
