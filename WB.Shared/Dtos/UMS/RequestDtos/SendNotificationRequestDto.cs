using System.ComponentModel.DataAnnotations.Schema;

namespace WB.Shared.Dtos.UMS.RequestDtos
{
    public class SendNotificationRequestDto
    {
        public int EventId { set; get; }
        public string EntityId { set; get; }
        public string? Action { set; get; }
        public string EntityName { set; get; }
        public string SenderId { set; get; }
        public string ReceiverId { set; get; }
        public string Culture { set; get; }
        public int ModuleId { set; get; }
        public bool IsReceiverMasterAdmin { get; set; }
        public string Status { get; set; }

        [NotMapped]
        public string SiteName { get; set; }
    }
}
