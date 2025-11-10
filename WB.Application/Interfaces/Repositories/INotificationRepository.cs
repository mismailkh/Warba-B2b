using WB.Shared.Dtos.UMS.RequestDtos;
using WB.Shared.Dtos.UMS.ResponseDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WB.Domain.Entities.Notification;

namespace WB.Application.Interfaces.Repositories
{
    public interface INotificationRepository
    {

        Task SendNotification(List<Notification> bulkNotifications);
        Task<List<NotificationResponseDto>> GetUnreadNotifications(string culture, string userId);
        Task<List<NotificationResponseDto>> GetAllNotifications(NotificationAdvanceSearchRequestDto advanceSearch);
        Task<List<NotificationEventResponseDto>> GetEventList(string culture);
        Task<List<NotificationTemplateResponseDto>> GetTemplateListByEventId(string culture, int eventId);
        Task MarkAllNotificationAsRead(List<Guid> notificationIds);
        Task MarkNotificationAsRead(Guid notificationId);

        Task DeleteNotification(Guid notificationId, string username);
        Task UpdateEventTemplate(UpdateNotificationTemplateRequestDto template);
        Task UpdateEvent(UpdateNotificationEventRequestDto eventt);
        Task<List<NotificationEventPlaceholders>> GetEventPlaceHolders(int eventId);

    }
}
