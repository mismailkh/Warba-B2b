using WB.Shared.Dtos.General.RequestDtos;
using WB.Shared.Dtos.General.ResponseDtos;
using WB.Shared.Dtos.UMS.RequestDtos;
using WB.Shared.Dtos.UMS.ResponseDtos;

namespace WB.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<List<UserListResponseDto>> GetUsersList(string culture);
        Task<UserDetailResponseDto> GetUserDetail(string culture, string userId);
        Task<List<ProcessLogsResponseDto>> GetProcessLogsByUserId(string userId);
        Task CreateAdminUser(CreateAdminRequestDto adminRequest);
        Task SaveUser(SaveUserRequestDto userRequest);
        Task SaveUserAccess(SaveUserAccessRequestDto userAccessRequest);
        Task UpdateUserStatus(UpdateUserStatusRequestDto userStatusRequest);

    }
}
