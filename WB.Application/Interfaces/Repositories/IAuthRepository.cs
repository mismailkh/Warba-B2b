using WB.Shared.Dtos;
using WB.Shared.Dtos.General.ResponseDtos;
using WB.Shared.Dtos.UMS.RequestDtos;
using WB.Shared.Dtos.UMS.ResponseDtos;

namespace WB.Application.Interfaces.Repositories
{
    public interface IAuthRepository
    {
        Task AdminChangeUserPassword(AdminChangeUserPasswordRequestDto userPasswordRequest);
        Task<List<ClaimSucessResponse>> GetUserAssignedClaims(string userId);
        Task<UserClaimsRolesResponseDto> GetUserLatestClaimAndRoles(string userId);
        Task<SecurityStampResponseDto> GetSecurityStampByEmail(string userId);
    }
}
