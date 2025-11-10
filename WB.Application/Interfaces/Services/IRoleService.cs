using WB.Shared.Dtos.UMS.RequestDtos;
using WB.Shared.Dtos.UMS.ResponseDtos;
using System.Threading.Tasks;

namespace WB.Application.Interfaces.Services
{
    public interface IRoleService
    {
        Task<List<RolesListResponseDto>> GetRolesList(string culture);
        Task<RoleDetailResponseDto> GetRoleDetails(string culture, string roleId);
        Task SaveRole(SaveRoleRequestDto roleRequest);
        Task UpdateRoleStatus(UpdateRoleStatusRequestDto updateRoleStatusRequest);
        Task SaveUserRole(SaveRoleAssignmentRequestDto roleAssignmentRequest);
        Task<List<UserAssignedRoleResponseDto>> GetUserRoles(string userId);
    }
}
