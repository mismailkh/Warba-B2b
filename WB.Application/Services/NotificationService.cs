using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using WB.Application.Interfaces.Repositories;
using WB.Application.Interfaces.Services;
using WB.Application.SignalR;
using WB.Domain.Entities.Notification;
using WB.Domain.Entities.Ums;
using WB.Shared.Configs;
using WB.Shared.Dtos.UMS.RequestDtos;
using WB.Shared.Dtos.UMS.ResponseDtos;
using static WB.Shared.Enums.NotificationEnums;

namespace WB.Application.Services
{
    public class NotificationService : INotificationService
    {

        private readonly INotificationRepository _notificationRepository;
        private readonly IHubContext<NotificationsHub, INotificationClient> _notificationSignalRClient;
        private readonly IMapper _mapper;
        private readonly IServiceScopeFactory _serviceScopeFactory;


        public NotificationService(INotificationRepository notificationRepository, IHubContext<NotificationsHub, INotificationClient> notificationSignalRClient, IMapper mapper, IServiceScopeFactory serviceScopeFactory)
        {
            _notificationRepository = notificationRepository;
            _notificationSignalRClient = notificationSignalRClient;
            _serviceScopeFactory = serviceScopeFactory;
            _mapper = mapper;
        }

        #region Send Notification
        public async Task SendNotification(List<SendNotificationRequestDto> bulkNotificationRequests)
        {
            try
            {
                //List<Notification> bulkNotifications = new List<Notification>();
                //foreach (var notificationRequest in bulkNotificationRequests)
                //{
                //    var notification = _mapper.Map<Notification>(notificationRequest);
                //    notification.NotificationParameter = new NotificationParameterRequestDto();
                //    await AssignEntityName(notification, notificationRequest);
                //    if (notificationRequest.IsReceiverMasterAdmin)
                //        notification.ReceiverId = await FetchMasterAdmin();

                //    using (var scope = _serviceScopeFactory.CreateScope())
                //    {
                //        var _userDbContext = scope.ServiceProvider.GetRequiredService<IGenericRepository<User>>();
                //        var _pInfoDbContext = scope.ServiceProvider.GetRequiredService<IGenericRepository<UserPersonalInformation>>();
                //        var loggedUser = _userDbContext.Get(x => x.Id == notification.CreatedBy).Result.FirstOrDefault();
                //        if (loggedUser != null)
                //        {
                //            notification.SenderId = loggedUser.Id;
                //            notification.NotificationParameter.SenderName = _pInfoDbContext.Get(x => x.UserId == notification.SenderId).Result.Select(x => x.FullName + "/" + x.FullName).FirstOrDefault();
                //        }
                //        notification.NotificationParameter.ReceiverName = _pInfoDbContext.Get(x => x.UserId == notification.ReceiverId).Result.Select(x => x.FullName + "/" + x.FullName).FirstOrDefault();
                //        notification.NotificationParameter.CreatedDate = notification.CreatedDate;

                //        notification.NotificationURL = await CreateNotificationURL(notification.Action, notification.EntityId);
                //        bulkNotifications.Add(notification);
                //    }
                //}
                //await _notificationRepository.SendNotification(bulkNotifications);
                //await SendNotificationViaSignalR(bulkNotifications);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<string> FetchMasterAdmin()
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var _pInfoDbContext = scope.ServiceProvider.GetRequiredService<IGenericRepository<UserRoles>>();
                return _pInfoDbContext.Get(x => x.RoleId == SystemRoles.SuperAdmin).Result.Select(x => x.UserId).FirstOrDefault();
            }
        }

        //public async Task AssignEntityName(Notification notification, SendNotificationRequestDto data)
        //{
        //    if (notification.EventId == (int)NotificationEventEnum.SharePromoCode)
        //    {
        //        notification.NotificationParameter.Code = data.EntityName;
        //    }
        //    else if (notification.EventId == (int)NotificationEventEnum.SharedFolder)
        //    {
        //        notification.NotificationParameter.FolderName = data.EntityName;
        //    }
        //    else if (notification.EventId == (int)NotificationEventEnum.AddProjectMember)
        //    {
        //        notification.NotificationParameter.ProjectName = data.EntityName;
        //    }
        //    else if (notification.EventId == (int)NotificationEventEnum.AssignRole)
        //    {
        //        notification.NotificationParameter.RoleName = data.EntityName;
        //    }
        //    else if (notification.EventId == (int)NotificationEventEnum.AddSiteUser)
        //    {
        //        notification.NotificationParameter.SiteName = data.SiteName;
        //    }
        //    else if (notification.EventId == (int)NotificationEventEnum.SiteDeactivation)
        //    {
        //        notification.NotificationParameter.SiteName = data.SiteName;
        //    }
        //    else if (notification.EventId == (int)NotificationEventEnum.SiteActivation)
        //    {
        //        notification.NotificationParameter.SiteName = data.SiteName;
        //    }

        //    else if (notification.EventId == (int)NotificationEventEnum.ActionAgainstLicensePurchaseRequest
        //        || notification.EventId == (int)NotificationEventEnum.ActionAgainstSubscriptionPurchaseRequest)
        //    {
        //        notification.NotificationParameter.Status = data.Status;
        //    }
        //}

        public async Task<string> CreateNotificationURL(string action, string entityId)
        {
            try
            {
                return $"/{action.ToLower()}{"/"}{entityId?.ToLower()}";
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region Get Unread Notifications
        public async Task<List<NotificationResponseDto>> GetUnreadNotifications(string culture, string userId)
        {
            try
            {
                return await _notificationRepository.GetUnreadNotifications(culture, userId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
        #endregion

        #region Get All Notifications
        public async Task<List<NotificationResponseDto>> GetAllNotifications(NotificationAdvanceSearchRequestDto advanceSearch)
        {
            try
            {
                return await _notificationRepository.GetAllNotifications(advanceSearch);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region Mark All Notifications as Read
        public async Task MarkAllNotificationAsRead(List<Guid> notificationIds)
        {
            try
            {
                await _notificationRepository.MarkAllNotificationAsRead(notificationIds);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        #endregion

        #region Mark Notification as Read
        public async Task MarkNotificationAsRead(Guid notificationId)
        {
            try
            {
                await _notificationRepository.MarkNotificationAsRead(notificationId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        #endregion

        #region Delete Notification
        public async Task DeleteNotification(Guid notificationId, string username)
        {
            try
            {
                await _notificationRepository.DeleteNotification(notificationId, username);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        #endregion
        #region SignalR Notifications
        private async Task SendNotificationViaSignalR(List<Notification> bulkNotifications)
        {
            try
            {
                foreach (var notification in bulkNotifications)
                {
                    NotificationResponseDto notificationResponse = new NotificationResponseDto();
                    _mapper.Map(notification, notificationResponse);
                    await _notificationSignalRClient.Clients.User(notification.ReceiverId).SendNotification(notificationResponse);
                }
            }

            catch (Exception ex)
            {

            }
        }
        #endregion

        #region Get Event List
        public async Task<List<NotificationEventResponseDto>> GetEventList(string culture)
        {
            try
            {
                return await _notificationRepository.GetEventList(culture);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region Get Event List
        public async Task<List<NotificationTemplateResponseDto>> GetTemplateListByEventId(string culture, int eventId)
        {
            try
            {
                return await _notificationRepository.GetTemplateListByEventId(culture, eventId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region Update Event Template
        public async Task UpdateEventTemplate(UpdateNotificationTemplateRequestDto template)
        {
            try
            {
                await _notificationRepository.UpdateEventTemplate(template);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region Update Event 
        public async Task UpdateEvent(UpdateNotificationEventRequestDto eventt)
        {
            try
            {
                await _notificationRepository.UpdateEvent(eventt);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region Get Event Placeholders
        public async Task<List<NotificationEventPlaceHolderResponseDto>> GetEventPlaceHolders(int eventId)
        {
            try
            {
                return _mapper.Map<List<NotificationEventPlaceHolderResponseDto>>(await _notificationRepository.GetEventPlaceHolders(eventId));
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion
    }
}
