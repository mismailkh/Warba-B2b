using AutoMapper;
using AutoMapper.Internal;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WB.Application.Interfaces.Repositories;
using WB.Domain.Entities.Ums;
using WB.Infrastructure.DbContext;
using WB.Shared.Dtos.UMS.RequestDtos;
using WB.Shared.Dtos.UMS.ResponseDtos;

namespace WB.Infrastructure.Repository
{
    public class GroupRepository(DatabaseContext _dbContext, IMapper _mapper, UserManager<User> _userManager)
    {
        //public async Task<List<GroupListResponseDto>> GetGroupsList(string culture)
        //{
        //    try
        //    {
        //        return await _dbContext.GroupListResponseDto.FromSql($"select * from ums.fGroupList({culture})").AsNoTracking().ToListAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}
        //public async Task<Group> GetGroupDetail(string culture, Guid groupId)
        //{
        //    try
        //    {
        //        return await _dbContext.Group
        //            .Include(g => g.UserGroup)
        //            .Include(g => g.GroupClaims)
        //            .AsNoTracking()
        //            .FirstOrDefaultAsync(u => u.Id == groupId);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}
        //public async Task SaveGroup(SaveGroupRequestDto groupRequest)
        //{
        //    using (_dbContext)
        //    {
        //        using (var transaction = await _dbContext.Database.BeginTransactionAsync())
        //        {
        //            try
        //            {
        //                Group group = _mapper.Map<Group>(groupRequest);
        //                if (groupRequest.Id == null)
        //                {
        //                    await _dbContext.Group.AddAsync(group);
        //                }
        //                else
        //                {
        //                    group = await _dbContext.Group.FindAsync(groupRequest.Id);
        //                    _mapper.Map(groupRequest, group);
        //                    _dbContext.Group.Update(group);
        //                }

        //                await _dbContext.SaveChangesAsync();
        //                await transaction.CommitAsync();
        //            }
        //            catch (Exception ex)
        //            {
        //                await transaction.RollbackAsync();
        //                throw;
        //            }
        //        }
        //    }
        //}
        //public async Task<List<string>> SaveGroupAccess(SaveGroupAccessRequestDto groupAccessRequest)
        //{
        //    using (_dbContext)
        //    {
        //        using (var transaction = await _dbContext.Database.BeginTransactionAsync())
        //        {
        //            try
        //            {
        //                var IsClaimUpdate = await SaveGroupClaims(_dbContext, groupAccessRequest);
        //                var modifiedUserlist = await SaveGroupUsers(_dbContext, groupAccessRequest, IsClaimUpdate);
        //                await _dbContext.SaveChangesAsync();
        //                await transaction.CommitAsync();
        //                return modifiedUserlist;
        //            }
        //            catch (Exception ex)
        //            {
        //                await transaction.RollbackAsync();
        //                throw;
        //            }
        //        }
        //    }
        //}
        //private async Task<List<string>> SaveGroupUsers(DatabaseContext dbContext, SaveGroupAccessRequestDto groupAccessRequest, bool hasClaimsChanged)
        //{
        //    try
        //    {
        //        var groupUsers = dbContext.UserGroup.Where(x => x.GroupId == groupAccessRequest.GroupId).ToList();

        //        var currentUserIds = groupUsers.Select(x => x.UserId).ToList();
        //        var newUserIds = groupAccessRequest.UsersList.Select(x => x.Id).ToList();
        //        var usersToAdd = newUserIds.Except(currentUserIds).ToList();
        //        var usersToRemove = currentUserIds.Except(newUserIds).ToList();

        //        if (usersToRemove.Any())
        //        {
        //            dbContext.UserGroup.RemoveRange(groupUsers.Where(x => usersToRemove.Contains(x.UserId)));
        //        }

        //        foreach (var userId in usersToAdd)
        //        {
        //            UserGroup userGroup = new UserGroup
        //            {
        //                GroupId = groupAccessRequest.GroupId,
        //                UserId = userId,
        //                CreatedBy = groupAccessRequest.CreatedBy,
        //                CreatedDate = DateTime.Now,
        //            };
        //            await dbContext.UserGroup.AddAsync(userGroup);
        //        }
        //        if (hasClaimsChanged)
        //            return currentUserIds.ToList();
        //        return usersToAdd.Concat(usersToRemove).ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}
        //private async Task<bool> SaveGroupClaims(DatabaseContext dbContext, SaveGroupAccessRequestDto groupAccessRequest)
        //{
        //    try
        //    {
        //        var groupClaims = dbContext.GroupClaims.Where(x => x.GroupId == groupAccessRequest.GroupId).ToList();

        //        var currentClaimIds = groupClaims.Select(x => x.ClaimId).ToList();
        //        var newClaimIds = groupAccessRequest.ClaimsList.Select(x => x.Id).ToList();
        //        var claimToAdd = newClaimIds.Except(currentClaimIds).ToList();
        //        var claimToRemove = currentClaimIds.Except(newClaimIds).ToList();

        //        if (claimToRemove.Any())
        //        {
        //            dbContext.GroupClaims.RemoveRange(groupClaims.Where(x => claimToRemove.Contains(x.ClaimId)));
        //        }
        //        foreach (var claim in claimToAdd)
        //        {
        //            GroupClaims groupClaim = new GroupClaims
        //            {
        //                GroupId = groupAccessRequest.GroupId,
        //                ClaimId = claim,
        //                CreatedBy = groupAccessRequest.CreatedBy,
        //                CreatedDate = DateTime.Now,
        //            };
        //            await dbContext.GroupClaims.AddAsync(groupClaim);
        //        }
        //        return claimToAdd.Concat(claimToRemove).Any();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}
    }
}
