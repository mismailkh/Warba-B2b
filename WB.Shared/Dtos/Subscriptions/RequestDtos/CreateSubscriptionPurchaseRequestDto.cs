namespace WB.Shared.Dtos.Subscriptions.RequestDtos
{
    public class CreateSubscriptionPurchaseRequestDto
    {
        public Guid SiteId { get; set; }
        public Guid? PromoCodeId { get; set; }
        public string? Name { get; set; }
        public int Sessions { get; set; }
        public int Duration { get; set; } = 1;
        public decimal Amount { get; set; }
        public string CreatedBy { get; set; } = default!;
        public string? ModifiedBy { get; set; }
        public int RequestTypeId { get; set; }
        public int StatusId { get; set; }

    }
}
