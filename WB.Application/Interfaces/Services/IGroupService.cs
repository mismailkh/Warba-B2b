using WB.Shared.Dtos.General.RequestDtos;
using WB.Shared.Dtos.General.ResponseDtos;
using WB.Shared.Dtos.UMS.RequestDtos;
using WB.Shared.Dtos.UMS.ResponseDtos;

namespace WB.Application.Interfaces.Services
{
    public interface IGroupService
    {
        Task<List<GroupListResponseDto>> GetGroupsList(string culture);
        Task<GroupDetailResponseDto> GetGroupDetail(string culture, Guid groupId);
        Task SaveGroup(SaveGroupRequestDto groupRequest);
        Task SaveGroupAccess(SaveGroupAccessRequestDto groupAccessRequest);
    }
}
