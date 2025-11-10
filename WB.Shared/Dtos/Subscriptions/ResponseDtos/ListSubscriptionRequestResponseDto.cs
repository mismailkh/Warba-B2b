namespace WB.Shared.Dtos.Subscriptions.ResponseDtos
{
    public class ListSubscriptionRequestResponseDto
    {
        public Guid Id { get; set; }
        public Guid? SiteId { get; set; }
        public string SiteName { get; set; }
        public DateTime RequestDate { get; set; }
        public int RequestStatusId { get; set; } // Approve,Reject
        public decimal Amount { get; set; }
        public int Sessions { get; set; }
        public int Duration { get; set; }
        public string RequestedBy { get; set; }

    }
}
