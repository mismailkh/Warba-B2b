using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WB.Application.Interfaces.Repositories;
using WB.Domain.Entities.Notification;
using WB.Infrastructure.DbContext;
using WB.Shared.Dtos.UMS.RequestDtos;
using WB.Shared.Dtos.UMS.ResponseDtos;
using WB.Shared.Enums;
using static WB.Shared.Enums.NotificationEnums;

namespace WB.Infrastructure.Repository
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly DatabaseContext _dbContext;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IMapper _mapper;
        public NotificationRepository(DatabaseContext dbContext, IServiceScopeFactory serviceScopeFactory, IMapper mapper)
        {
            _dbContext = dbContext;
            _serviceScopeFactory = serviceScopeFactory;
            _mapper = mapper;
        }

        #region Send Notification
        public async Task SendNotification(List<Notification> bulkNotifications)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            using var dbContext=scope.ServiceProvider.GetService<DatabaseContext>();
            int index;
            using (dbContext)
            {
                using (var transaction = dbContext.Database.BeginTransaction())
                {
                    try
                    {

                        foreach (var notification in bulkNotifications.ToList())
                        {
                            index=bulkNotifications.IndexOf(notification);
                            if (await dbContext.NotificationEvents.Where(x => x.EventId == notification.EventId && x.IsActive).AnyAsync())
                            {


                                var notificationTemplates = await dbContext.NotificationTemplates.Where(x => x.EventId == notification.EventId && x.IsActive).ToListAsync();

                                foreach (var notificationTemplate in notificationTemplates)
                                {
                                    notification.NotificationTemplateId = notificationTemplate.TemplateId;
                                    //(notification.NotificationMessageEn, notification.NotificationMessageAr) = await CreateNotificationMessage(notificationTemplate, notification.NotificationParameter, dbContext);
                                    bulkNotifications[index] = notification;
                                }
                            }
                        }
                        await SendBrowserNotification(bulkNotifications, dbContext);
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                    }
                }
            }
        }

        public async Task<(string, string)> CreateNotificationMessage(NotificationTemplate notificationTemplate, NotificationParameterRequestDto entity, DatabaseContext _dbContext)
        {
            try
            {
                var placeholders = await _dbContext.NotificationEventPlaceholders.Where(x => x.EventId == notificationTemplate.EventId || x.EventId == null).ToListAsync();
                var bodyEN = FillPlaceHolders(placeholders, notificationTemplate.BodyEn, entity, "en");
                var bodyAR = FillPlaceHolders(placeholders, notificationTemplate.BodyAr, entity, "ar-KW");
                return (bodyEN, bodyAR);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private string FillPlaceHolders(List<NotificationEventPlaceholders> placeholders, string message, NotificationParameterRequestDto entity, string lang)
        {

            if (message == null)
            {
                //return _resourceManager.GetString("DefaultMessage", lang);
            }
            foreach (var item in placeholders)
            {
                var placeHolderName = item.PlaceHolderName;
                switch (placeHolderName)
                {
                    case var placeHolder when placeHolder == NotificationPlaceholderEnum.RecieverName.GetDisplayName():
                        message = string.IsNullOrEmpty(entity.ReceiverName) ? "" : (lang == "en" ? entity.ReceiverName.Split("/")[0] : entity.ReceiverName.Split("/")[1]);
                        break;
                    case var placeHolder when placeHolder == NotificationPlaceholderEnum.CreatedDate.GetDisplayName():
                        var createdDate = entity.CreatedDate;
                        message = message.Replace(placeHolder, createdDate.ToString());
                        break;
                    case var placeHolder when placeHolder == NotificationPlaceholderEnum.SenderName.GetDisplayName():
                        var senderName = string.IsNullOrEmpty(entity.SenderName) ? "" : (lang == "en" ? entity.SenderName.Split("/")[0] : entity.SenderName.Split("/")[1]);
                        message = message.Replace(placeHolder, senderName);
                        break;
                    case var placeHolder when placeHolder == NotificationPlaceholderEnum.RoleName.GetDisplayName():
                        var roleName = string.IsNullOrEmpty(entity.RoleName) ? "" : (lang == "en" ? entity.RoleName.Split("/")[0] : entity.RoleName.Split("/")[1]);
                        message = message.Replace(placeHolder, roleName);
                        break;

                    default: break;
                }
            }
            return message;
        }
        public async Task SendBrowserNotification(List<Notification> bulkNotifications, DatabaseContext _dbContext)
        {
            try
            {
                foreach (var notification in bulkNotifications)
                {
                    notification.NotificationStatusId = (int)NotificationStatusEnum.Unread;
                }
                await _dbContext.Notifications.AddRangeAsync(bulkNotifications);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex) { throw; }
        }
        #endregion

        #region Get Unread Notifications
        public async Task<List<NotificationResponseDto>> GetUnreadNotifications(string culture, string userId)
        {
            try
            {
                return await _dbContext.NotificationListResponseDto.FromSql($"select * from notif.fnotificationslist({culture},{userId},{false},{null},{null},{null},{null},{null})").AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region Get All Notifications
        public async Task<List<NotificationResponseDto>> GetAllNotifications(NotificationAdvanceSearchRequestDto advanceSearch)
        {
            try
            {
                var function = $"select * from notif.fnotificationslist({advanceSearch.Culture},{advanceSearch.UserId},{advanceSearch.ViewAll},{advanceSearch.SenderName},{advanceSearch.NotificationMessage},{advanceSearch.FromDate},{advanceSearch.ToDate},{advanceSearch.TabValue})";
                var notifications = await _dbContext.NotificationListResponseDto.FromSql($"select * from notif.fnotificationslist({advanceSearch.Culture},{advanceSearch.UserId},{advanceSearch.ViewAll},{advanceSearch.SenderName},{advanceSearch.NotificationMessage},{advanceSearch.FromDate},{advanceSearch.ToDate},{advanceSearch.TabValue})").AsNoTracking().ToListAsync();
                return notifications;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region Mark All Notification As Read
        public async Task MarkAllNotificationAsRead(List<Guid> notificationIds)
        {
            foreach (var notificationId in notificationIds)
            {
                await MarkNotificationAsRead(notificationId);
            }
        }

        public async Task MarkNotificationAsRead(Guid notificationId)
        {
            try
            {

                var result = await _dbContext.Notifications.Where(x => x.NotificationId == notificationId).FirstOrDefaultAsync();
                if (result != null)
                {
                    result.NotificationStatusId = (int)NotificationStatusEnum.Read;
                    result.ReadDate = DateTime.Now;
                    _dbContext.Update(result);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region Delete Notification
        public async Task DeleteNotification(Guid notificationId, string username)
        {
            try
            {

                var result = await _dbContext.Notifications.Where(x => x.NotificationId == notificationId).FirstOrDefaultAsync();
                if (result != null)
                {
                    result.IsDeleted = true;
                    result.DeletedDate = DateTime.Now;
                    result.DeletedBy = username;
                    _dbContext.Update(result);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region Get Event List
        public async Task<List<NotificationEventResponseDto>> GetEventList(string culture)
        {
            try
            {
                var function = $"select * from notif.fnotificationeventslist('{culture}')";
                var eventList = await _dbContext.EventListResponseDto.FromSqlRaw(function).ToListAsync();
                return eventList;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region Get Template List Event Id
        public async Task<List<NotificationTemplateResponseDto>> GetTemplateListByEventId(string culture, int eventId)
        {
            try
            {
                var function = $"select * from notif.fnotificationtemplatelistbyevent('{culture}',{eventId})";
                var templateList = await _dbContext.TemplateListResponseDto.FromSqlRaw(function).ToListAsync();
                return templateList;
            }
            catch
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
                var result = _dbContext.NotificationTemplates.Where(x => x.TemplateId == template.TemplateId).FirstOrDefault();
                if (result != null)
                {
                    _mapper.Map(template,result);
                    Console.WriteLine(result.ToString());
                    _dbContext.Update(result);

                    await _dbContext.SaveChangesAsync();
                }
            }
            catch
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
                var result = _dbContext.NotificationEvents.Where(x => x.EventId== eventt.EventId).FirstOrDefault();
                if (result != null)
                {
                    _mapper.Map(eventt, result);
                    Console.WriteLine(result.ToString);
                    _dbContext.Update(result);

                    await _dbContext.SaveChangesAsync();
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region Get Event Placeholders
        public async Task<List<NotificationEventPlaceholders>> GetEventPlaceHolders(int eventId)
        {
            try
            {
                return await _dbContext.NotificationEventPlaceholders.Where(x => x.EventId == eventId).ToListAsync();
            }
            catch
            {
                throw;
            }
        }
        #endregion
    }
}
