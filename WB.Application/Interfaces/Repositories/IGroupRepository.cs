using WB.Shared.Dtos.General.RequestDtos;
using WB.Shared.Dtos.General.ResponseDtos;
using WB.Shared.Dtos.UMS.RequestDtos;
using WB.Shared.Dtos.UMS.ResponseDtos;
using WB.Domain.Entities.Ums;

namespace WB.Application.Interfaces.Repositories
{
    public interface IGroupRepository
    {
        Task<List<GroupListResponseDto>> GetGroupsList(string culture);
        Task<Group> GetGroupDetail(string culture, Guid groupId);
        Task SaveGroup(SaveGroupRequestDto groupRequest);
        Task<List<string>> SaveGroupAccess(SaveGroupAccessRequestDto groupAccessRequest);
    }
}
