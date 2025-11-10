using WB.Shared.Dtos;
using WB.Shared.Dtos.General.ResponseDtos;
using WB.Shared.Dtos.UMS.RequestDtos;
using WB.Shared.Dtos.UMS.ResponseDtos;

namespace WB.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<AuthenticationResult> Login(string email, string password, string culture, bool hasTranslations);
        Task<List<ClaimListResponseDto>> GetClaimsList(string culture);
        Task<List<ClaimSucessResponse>> GetUserAssignedClaims(string userId);
        Task<UserClaimsRolesResponseDto> GetUserLatestClaimAndRoles(string userId);
        Task<SecurityStampResponseDto> GetSecurityStampByEmail(string userId);
        Task AdminChangeUserPassword(AdminChangeUserPasswordRequestDto userPasswordRequest);
        Task<bool> FindUserByEmail(string email);
        Task<bool> FindUserByUserName(string username);
    }
}
