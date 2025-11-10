namespace WB.Shared.Dtos.Subscriptions.ResponseDtos
{
    public class SiteSubscriptionPurchaseResponseDto
    {
        public Guid Id { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int StatusId { get; set; }
        public string SiteType { get; set; }
        public int SiteTypeId { get; set; }
        public int Duration { get; set; }
        public decimal Amount { get; set; }
        public int Sessions { get; set; }
        public int TotalSessions { get; set; }
        public int CarryOverSessions { get; set; }
        public int RemainingSessions { get; set; }
        public int UsedSessions { get; set; }
     }
}
