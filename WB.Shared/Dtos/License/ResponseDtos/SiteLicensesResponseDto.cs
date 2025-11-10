namespace WB.Shared.Dtos.License.ResponseDtos
{
    public class SiteLicensesResponseDto
    {
        public Guid Id { get; set; }
        public Guid LicenseId { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string Status { get; set; }
        public int StatusId { get; set; }
        public string SiteType { get; set; }
        public int SiteTypeId { get; set; }
        public int GracePeriod { get; set; }
        public int Duration { get; set; }
        public decimal Amount { get; set; }
        public int MHPSeats { get; set; }
        public int OperationalSeats { get; set; }
        public int AdministrationalSeats { get; set; }
        public int LicenseTypeId { get; set; }
    }
}
