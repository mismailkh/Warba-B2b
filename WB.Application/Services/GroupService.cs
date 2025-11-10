using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using WB.Application.Interfaces.Repositories;
using WB.Application.Interfaces.Services;
using WB.Application.SignalR;
using WB.Shared.Dtos.UMS.RequestDtos;
using WB.Shared.Dtos.UMS.ResponseDtos;
using WB.Shared.Enums;
using static WB.Shared.Enums.GeneralEnums;

namespace WB.Application.Services
{
    public class GroupService(IGroupRepository _groupRepository, IMapper _mapper, ILoggingService _iLoggingService, IHttpContextAccessor _httpContext, IHubContext<NotificationsHub, INotificationClient> _notificationSignalRClient) : IGroupService
    {
        public async Task<List<GroupListResponseDto>> GetGroupsList(string culture)
        {
            try
            {
                return await _groupRepository.GetGroupsList(culture);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<GroupDetailResponseDto> GetGroupDetail(string culture, Guid groupId)
        {
            try
            {
                return _mapper.Map<GroupDetailResponseDto>(await _groupRepository.GetGroupDetail(culture, groupId));
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task SaveGroup(SaveGroupRequestDto groupRequest)
        {
            try
            {
                await _groupRepository.SaveGroup(groupRequest);
                await _iLoggingService.CreateProcessLog(new SaveProcessLogRequestDto
                {
                    Module = ModuleEnum.Ums.GetDisplayName(),
                    Process = groupRequest.Id == null ? "Create_Group" : groupRequest.IsDeleted ? "Delete_Group" : "Update_Group",
                    Description = groupRequest.Id == null ? "New_Group_Created" : groupRequest.IsDeleted ? "Group_Deleted" : "Group_Updated",
                    LogDate = DateTime.Now,
                    IPAddress = _httpContext.HttpContext?.Connection?.RemoteIpAddress?.ToString(),
                    Token = _httpContext.HttpContext?.Request?.Headers["Authorization"].ToString()?.Replace("Bearer ", "")
                });
            }
            catch (Exception ex)
            {
                await _iLoggingService.SaveErrorLogDetails(new SaveErrorLogRequestDto
                {
                    Module = ModuleEnum.Ums.GetDisplayName(),
                    Operation = groupRequest.Id == null ? "Create_Group" : groupRequest.IsDeleted ? "Delete_Group" : "Update_Group",
                    Message = ex.Message,
                    StackTrace = ex.StackTrace,
                    Source = ex.Source,
                    LogDate = DateTime.Now,
                    IPAddress = _httpContext.HttpContext?.Connection?.RemoteIpAddress?.ToString(),
                    Token = _httpContext.HttpContext?.Request?.Headers["Authorization"].ToString()?.Replace("Bearer ", "")
                });
                throw;
            }
        }
        public async Task SaveGroupAccess(SaveGroupAccessRequestDto groupAccessRequest)
        {
            try
            {
                var users = await _groupRepository.SaveGroupAccess(groupAccessRequest);
                await Task.WhenAll(users.Select(userId => _notificationSignalRClient.Clients.User(userId).NotifyClaimsRolesUpdated(userId)));
                await _iLoggingService.CreateProcessLog(new SaveProcessLogRequestDto
                {
                    Module = ModuleEnum.Ums.GetDisplayName(),
                    Process = "Update_Group_Access",
                    Description = "Group_Access_Updated",
                    IPAddress = _httpContext.HttpContext?.Connection?.RemoteIpAddress?.ToString(),
                    LogDate = DateTime.Now,
                    Token = _httpContext.HttpContext?.Request?.Headers["Authorization"].ToString()?.Replace("Bearer ", "")
                });
            }
            catch (Exception ex)
            {
                await _iLoggingService.SaveErrorLogDetails(new SaveErrorLogRequestDto
                {
                    Module = ModuleEnum.Ums.GetDisplayName(),
                    Operation = "Save_Group_Access",
                    Message = ex.Message,
                    StackTrace = ex.StackTrace,
                    Source = ex.Source,
                    IPAddress = _httpContext.HttpContext?.Connection?.RemoteIpAddress?.ToString(),
                    LogDate = DateTime.Now,
                    Token = _httpContext.HttpContext?.Request?.Headers["Authorization"].ToString()?.Replace("Bearer ", "")
                });
                throw;
            }
        }
    }
}
