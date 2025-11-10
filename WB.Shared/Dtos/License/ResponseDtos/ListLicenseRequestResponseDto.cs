namespace WB.Shared.Dtos.License.ResponseDtos
{
    public class ListLicenseRequestResponseDto
    {
        public Guid PurchaseRequestId { get; set; }
        public Guid? SiteId { get; set; }
        public string SiteName { get; set; }
        public string? CurrentLicensePlanName { get; set; }
        public string? RequestLicensePlanName { get; set; }
        public DateTime RequestDate { get; set; }
        public int RequestStatusId { get; set; } // Approve,Reject
        public Guid? PreviousLicensePlanVersionId { get; set; }
        public Guid? LicensePlanVersionId { get; set; }
        public int? Price { get; set; }
        public int? DurationMonths { get; set; }

    }
}
