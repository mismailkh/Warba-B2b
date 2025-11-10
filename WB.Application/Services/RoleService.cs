using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using WB.Application.Interfaces.Repositories;
using WB.Application.Interfaces.Services;
using WB.Application.SignalR;
using WB.Shared.Dtos;
using WB.Shared.Dtos.UMS.RequestDtos;
using WB.Shared.Dtos.UMS.ResponseDtos;
using WB.Shared.Enums;
using static WB.Shared.Enums.GeneralEnums;
using static WB.Shared.Enums.NotificationEnums;

namespace WB.Application.Services
{
    public class RoleService(IRoleRepository _roleRepository, IMapper _mapper, ILoggingService _iLoggingService, IHttpContextAccessor _httpContext, INotificationService _notificationService, IHubContext<NotificationsHub, INotificationClient> _notificationSignalRClient, IAuthRepository _iAuthRepository) : IRoleService
    {
        public async Task<List<RolesListResponseDto>> GetRolesList(string culture)
        {
            try
            {
                return await _roleRepository.GetRolesList(culture);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<RoleDetailResponseDto> GetRoleDetails(string culture, string roleId)
        {
            try
            {
                var res = _mapper.Map<RoleDetailResponseDto>(await _roleRepository.GetRoleDetails(culture, roleId));
                return res;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task SaveRole(SaveRoleRequestDto roleRequest)
        {
            try
            {
                await _roleRepository.SaveRole(roleRequest);
                await _iLoggingService.CreateProcessLog(new SaveProcessLogRequestDto
                {
                    Module = ModuleEnum.Ums.GetDisplayName(),
                    Process = roleRequest.Id == null ? "Add_New_Role" : "Update_Role",
                    Description = roleRequest.Id == null ? "New_Role_Added" : "Role_Updated",
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
                    Operation = roleRequest.Id == null ? "Add_New_Role" : "Update_Role",
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

        public async Task UpdateRoleStatus(UpdateRoleStatusRequestDto updateRoleStatusRequest)
        {
            try
            {
                var userslist = await _roleRepository.UpdateRoleStatus(updateRoleStatusRequest);
                await Task.WhenAll(userslist.Select(userId => _notificationSignalRClient.Clients.User(userId).NotifyClaimsRolesUpdated(userId)));

                await _iLoggingService.CreateProcessLog(new SaveProcessLogRequestDto
                {
                    Module = ModuleEnum.Ums.GetDisplayName(),
                    Process = "Update_Role_Status",
                    Description = updateRoleStatusRequest.IsActive ? "Set_Role_Status_Active" : "Set_Role_Status_InActive",
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
                    Operation = "Update_Role_Status",
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

        public async Task SaveUserRole(SaveRoleAssignmentRequestDto roleAssignmentRequest)
        {
            try
            {
                var modifiedUserList = await _roleRepository.SaveUserRole(roleAssignmentRequest);
                await Task.WhenAll(modifiedUserList.Select(user => _notificationSignalRClient.Clients.User(user).NotifyClaimsRolesUpdated(user)));

                await _iLoggingService.CreateProcessLog(new SaveProcessLogRequestDto
                {
                    Module = ModuleEnum.Ums.GetDisplayName(),
                    Process = "Save_User_Role",
                    Description = roleAssignmentRequest.UsersList.Count > 1 ? "Users_Role_Updated" : "User_Role_Updated",
                    IPAddress = _httpContext.HttpContext?.Connection?.RemoteIpAddress?.ToString(),
                    LogDate = DateTime.Now,
                    Token = _httpContext.HttpContext?.Request?.Headers["Authorization"].ToString()?.Replace("Bearer ", "")
                });
                List<SendNotificationRequestDto> bulkNotificationRequests = new List<SendNotificationRequestDto>();
                var notificationRequest = new SendNotificationRequestDto();
                foreach (var receiver in roleAssignmentRequest.UsersList.Where(x => x.IsNotification == true))
                {
                    notificationRequest.EntityName = roleAssignmentRequest.RoleName;
                    notificationRequest.EntityId = roleAssignmentRequest.RoleId;
                    notificationRequest.EventId = (int)NotificationEventEnum.AssignRole;
                    notificationRequest.Culture = roleAssignmentRequest.Culture;
                    notificationRequest.SenderId = roleAssignmentRequest.CreatedBy;
                    notificationRequest.Action = "role-assignment";
                    notificationRequest.ModuleId = (int)ModuleEnum.Ums;
                    notificationRequest.ReceiverId = receiver.Id;
                    bulkNotificationRequests.Add(notificationRequest);
                    notificationRequest = new SendNotificationRequestDto();
                }
                await _notificationService.SendNotification(bulkNotificationRequests);

            }
            catch (Exception ex)
            {
                await _iLoggingService.SaveErrorLogDetails(new SaveErrorLogRequestDto
                {
                    Module = ModuleEnum.Ums.GetDisplayName(),
                    Operation = "Save_User_Role",
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

        public async Task<List<UserAssignedRoleResponseDto>> GetUserRoles(string userId)
        {
            try
            {
                return await _roleRepository.GetUserRoles(userId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
