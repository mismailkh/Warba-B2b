using WB.Domain.Entities.Ums;
using WB.Shared.Dtos.UMS.RequestDtos;
using WB.Shared.Dtos.UMS.ResponseDtos;

namespace WB.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<UserPersonalInformation> GetUserPersonalInformation(string userId);
        Task<List<UserListResponseDto>> GetUsersList(string culture);
        Task<UserDetailResponseDto> GetUserDetail(string culture, string userId);
        Task<List<ProcessLogsResponseDto>> GetProcessLogsByUserId(string userId);
        Task CreateAdminUser(CreateAdminRequestDto adminRequest);
        Task<SaveUserResponseDto> SaveUser(SaveUserRequestDto userRequest);
        Task SaveUserAccess(SaveUserAccessRequestDto userAccessRequest);
        Task UpdateUserStatus(UpdateUserStatusRequestDto userStatusRequest);

    }
}
