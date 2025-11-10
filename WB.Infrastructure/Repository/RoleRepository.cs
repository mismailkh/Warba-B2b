using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WB.Application.Interfaces.Repositories;
using WB.Domain.Entities.Ums;
using WB.Infrastructure.DbContext;
using WB.Shared.Dtos.UMS.RequestDtos;
using WB.Shared.Dtos.UMS.ResponseDtos;

namespace WB.Infrastructure.Repository
{
    public class RoleRepository(DatabaseContext _dbContext, IMapper _mapper, UserManager<User> _userManager) : IRoleRepository
    {
        #region Get Roles List
        public async Task<List<RolesListResponseDto>> GetRolesList(string culture)
        {
            try
            {
                return await _dbContext.RolesListResponseDto.FromSql($"select * from ums.fRolesList({culture})").AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region Get Role Details
        public async Task<Role> GetRoleDetails(string culture, string roleId)
        {
            try
            {
                return await _dbContext.Roles
                    .Include(g => g.UserRole)
                    .AsNoTracking().FirstOrDefaultAsync(u => u.Id == roleId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region Get User Role
        public async Task<List<UserAssignedRoleResponseDto>> GetUserRoles(string userId)
        {
            try
            {
                var userActiveRoles = await _dbContext.UserRoles.Include(ur => ur.Role)
                    .Where(ur => ur.UserId == userId && ur.Role.IsActive)
                    .Select(ur => ur.Role.Id).ToListAsync(); 

                return userActiveRoles.Select(roleId => new UserAssignedRoleResponseDto { RoleId = roleId }).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region Save Role
        public async Task SaveRole(SaveRoleRequestDto roleRequest)
        {
            using (_dbContext)
            {
                try
                {
                    Role role = _mapper.Map<Role>(roleRequest);
                    if (roleRequest.Id == null)
                    {
                        role.Id = Guid.NewGuid().ToString();
                        await _dbContext.Roles.AddAsync(role);
                    }
                    else
                    {
                        role = await _dbContext.Roles.FindAsync(roleRequest.Id);
                        _mapper.Map(roleRequest, role);
                        _dbContext.Roles.Update(role);
                    }
                    await _dbContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
        #endregion

        #region Update Role Status
        public async Task<List<string>> UpdateRoleStatus(UpdateRoleStatusRequestDto updateRoleStatusRequest)
        {
            using (_dbContext)
            {
                using (var transaction = await _dbContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        Role role = await _dbContext.Roles.FindAsync(updateRoleStatusRequest.RoleId);
                        if (role != null)
                        {
                            role.IsActive = updateRoleStatusRequest.IsActive;
                            role.ModifiedBy = updateRoleStatusRequest.ModifiedBy;
                            role.ModifiedDate = DateTime.Now;
                            await _dbContext.SaveChangesAsync();
                        }

                        var assignedUsers = _dbContext.UserRoles.Where(x => x.RoleId == updateRoleStatusRequest.RoleId).Select(x => x.UserId).ToList();
                        await transaction.CommitAsync();
                        return assignedUsers;
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

        #region Save User Role
        public async Task<List<string>> SaveUserRole(SaveRoleAssignmentRequestDto roleAssignmentRequest)
        {
            using (_dbContext)
            {
                using (var transaction = await _dbContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        var assignedUsers = _dbContext.UserRoles.Where(x => x.RoleId == roleAssignmentRequest.RoleId).ToList();
                        var currentUserIds = assignedUsers.Select(x => x.UserId).ToList();
                        var newUserIds = roleAssignmentRequest.UsersList.Select(x => x.Id).ToList();
                        var usersToAdd = newUserIds.Except(currentUserIds).ToList();
                        var usersToRemove = currentUserIds.Except(newUserIds).ToList();

                        if (usersToRemove.Any())
                        {
                            _dbContext.UserRoles.RemoveRange(assignedUsers.Where(x => usersToRemove.Contains(x.UserId)));
                        }
                        foreach (var userId in usersToAdd)
                        {
                            UserRoles updatedUserRole = new UserRoles
                            {
                                RoleId = roleAssignmentRequest.RoleId,
                                UserId = userId,
                                CreatedBy = roleAssignmentRequest.CreatedBy,
                                CreatedDate = DateTime.Now,
                            };
                            await _dbContext.UserRoles.AddAsync(updatedUserRole);
                        }
                        await _dbContext.SaveChangesAsync();
                        await transaction.CommitAsync();
                        return usersToAdd.Concat(usersToRemove).ToList();
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
        public async Task UpdateUserSecurityStamp(string usrId)
        {
            try
            {
                var identityUser = await _userManager.FindByIdAsync(usrId);
                var res = await _userManager.UpdateSecurityStampAsync(identityUser);
                if (!res.Succeeded)
                    throw new Exception();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion
    }
}
