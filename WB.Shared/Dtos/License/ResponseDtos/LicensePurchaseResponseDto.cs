namespace WB.Shared.Dtos.License.ResponseDtos
{
    public class LicensePurchaseResponseDto
    {
        public Guid Id { get; set; }
        public Guid? SiteId { get; set; }
        public string SiteName { get; set; }
        public DateTime RequestDate { get; set; }
        public int RequestStatusId { get; set; } // Approve,Reject
        public decimal Amount { get; set; }
        public int Duration { get; set; }
        public int MHPSeats { get; set; }
        public int OperationalSeats { get; set; }
        public int AdministrationalSeats { get; set; }
        public int LicenseTypeId { get; set; } // VR,MHMS,BOTH
        public string? RejectionReason { get; set; }
        public DateTime? RejectionDate { get; set; }
    }
}
