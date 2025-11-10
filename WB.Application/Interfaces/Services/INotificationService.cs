using WB.Shared.Dtos.UMS.RequestDtos;
using WB.Shared.Dtos.UMS.ResponseDtos;

namespace WB.Application.Interfaces.Services
{
    public interface INotificationService
    {
        public Task SendNotification(List<SendNotificationRequestDto> bulkNotificationRequests);
        public Task<List<NotificationResponseDto>> GetUnreadNotifications(string culture, string userId);
        public Task<List<NotificationResponseDto>> GetAllNotifications(NotificationAdvanceSearchRequestDto advanceSearch);

        public Task<List<NotificationEventResponseDto>> GetEventList(string culture);
        public Task<List<NotificationTemplateResponseDto>> GetTemplateListByEventId(string culture, int eventId);
        public Task MarkAllNotificationAsRead(List<Guid> notificationIds);
        public Task MarkNotificationAsRead(Guid notificationId);

        public Task DeleteNotification(Guid notificationId, string username);
        public Task UpdateEventTemplate(UpdateNotificationTemplateRequestDto template);
        public Task UpdateEvent(UpdateNotificationEventRequestDto eventt);
        public Task<List<NotificationEventPlaceHolderResponseDto>> GetEventPlaceHolders(int eventId);
    }
}
