using System.Security.Principal;

namespace WB.Shared.Dtos.General.RequestDtos
{
    public class SubscriptionUpdateRequestDto
    {
        public Guid SiteId { get; set; }
        public Guid? SiteSubscriptionId { get; set; }
        public Guid? SubscriptionPlanId { get; set; }
        public Guid? PromoCodeId { get; set; }
        public Guid? PurchaseRequestId { get; set; }
        public int StatusId { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public int RequestTypeId { get; set; }
        public string? ReviewerBy { get; set; }
        public int Duration { get; set; }
        public int Sessions { get; set; }
        public decimal Amount { get; set; }
        public string RejectionReason { get; set; }
        public string Culture { get; set; }

    }
}
