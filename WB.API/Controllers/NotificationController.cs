using Microsoft.AspNetCore.Mvc;
using WB.Application.Interfaces.Services;
using WB.Shared.Dtos;
using WB.Shared.Dtos.UMS.RequestDtos;

namespace WB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        #region Get Unread

        [MapToApiVersion("1.1")]
        [HttpGet("GetUnreadNotifications")]

        public async Task<IActionResult> GetUnreadNotifications(string culture, string userId)
        {
            try
            {
                var result = await _notificationService.GetUnreadNotifications(culture, userId);
                if (result != null)
                {
                    return Ok(result);
                }
                return NotFound(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new BadRequestResponse { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }

        }
        #endregion

        #region Get All

        [MapToApiVersion("1.1")]
        [HttpPost("GetAllNotifications")]

        public async Task<IActionResult> GetAllNotifications(NotificationAdvanceSearchRequestDto advanceSearch)
        {
            try
            {
                return Ok(await _notificationService.GetAllNotifications(advanceSearch));
            }

            catch (Exception ex)
            {
                return BadRequest(new BadRequestResponse { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }

        }
        #endregion

        #region Save

        [MapToApiVersion("1.1")]
        [HttpPost("SendNotification")]
        public async Task<IActionResult> SendNotification(List<SendNotificationRequestDto> bulkNotification)
        {
            try
            {
                await _notificationService.SendNotification(bulkNotification);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new BadRequestResponse { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }

        #endregion

        #region Mark All notifications as Read

        [MapToApiVersion("1.1")]
        [HttpPost("MarkAllNotificationAsRead")]
        public async Task<IActionResult> MarkAllNotificationAsRead(List<Guid> notificationIds)
        {
            try
            {
                await _notificationService.MarkAllNotificationAsRead(notificationIds);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new BadRequestResponse { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }
        #endregion
        #region Mark Single Notification as Read
        [MapToApiVersion("1.1")]
        [HttpGet("MarkNotificationAsRead")]
        public async Task<IActionResult> MarkNotificationAsRead(Guid notificationId)
        {
            try
            {
                await _notificationService.MarkNotificationAsRead(notificationId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new BadRequestResponse { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }
        #endregion

        #region Delete Notification
        [MapToApiVersion("1.1")]
        [HttpGet("DeleteNotification")]
        public async Task<IActionResult> DeleteNotification(Guid notificationId, string username)
        {
            try
            {
                await _notificationService.DeleteNotification(notificationId, username);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new BadRequestResponse { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }
        #endregion
        #region Get Event List
        [MapToApiVersion("1.1")]
        [HttpGet("GetEventList")]
        public async Task<IActionResult> GetEventList(string culture)
        {
            try
            {
                var result = await _notificationService.GetEventList(culture);
                if (result != null)
                {
                    return Ok(result);
                }
                return NotFound(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new BadRequestResponse { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }
        #endregion
        #region Get Template List By Event Id
        [MapToApiVersion("1.1")]
        [HttpGet("GetTemplateListByEventId")]
        public async Task<IActionResult> GetTemplateListByEventId(string culture, int eventId)
        {
            try
            {
                var result = await _notificationService.GetTemplateListByEventId(culture, eventId);
                if (result != null)
                {
                    return Ok(result);
                }
                return NotFound(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new BadRequestResponse { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }
        #endregion

        #region Edit Event Template
        [MapToApiVersion("1.1")]
        [HttpPost("UpdateEventTemplate")]
        public async Task<IActionResult> UpdateEventTemplate(UpdateNotificationTemplateRequestDto template)
        {
            try
            {
                await _notificationService.UpdateEventTemplate(template);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new BadRequestResponse { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }
        #endregion

        #region Edit Event
        [MapToApiVersion("1.1")]
        [HttpPost("UpdateEvent")]
        public async Task<IActionResult> UpdateEvent(UpdateNotificationEventRequestDto eventt)
        {
            try
            {
                await _notificationService.UpdateEvent(eventt);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new BadRequestResponse { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }
        #endregion

        #region Edit Event
        [MapToApiVersion("1.1")]
        [HttpGet("GetEventPlaceHolders")]
        public async Task<IActionResult> GetEventPlaceHolders(int eventId)
        {
            try
            {
                var result = await _notificationService.GetEventPlaceHolders(eventId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new BadRequestResponse { Message = ex.Message, InnerException = ex.InnerException?.Message });
            }
        }
        #endregion
    }
}