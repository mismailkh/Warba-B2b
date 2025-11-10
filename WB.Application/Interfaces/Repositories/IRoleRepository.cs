using WB.Shared.Dtos.UMS.RequestDtos;
using WB.Shared.Dtos.UMS.ResponseDtos;
using WB.Domain.Entities.Ums;

namespace WB.Application.Interfaces.Repositories
{
    public interface IRoleRepository
    {
        Task<List<RolesListResponseDto>> GetRolesList(string culture);
        Task<Role> GetRoleDetails(string culture, string roleId);
        Task<List<UserAssignedRoleResponseDto>> GetUserRoles(string userId);
        Task SaveRole(SaveRoleRequestDto roleRequest);
        Task<List<string>> UpdateRoleStatus(UpdateRoleStatusRequestDto updateRoleStatusRequest);
        Task<List<string>> SaveUserRole(SaveRoleAssignmentRequestDto roleAssignmentRequest);
    }
}
