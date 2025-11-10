using System.ComponentModel.DataAnnotations.Schema;

namespace WB.Shared.Dtos.License.RequestDtos
{
    public class LicenseUpdateRequestDto
    {
        public Guid SiteId { get; set; }
        public Guid? SiteLicenseId { get; set; }
        public Guid? LicensePlanId { get; set; }
        public Guid? PromoCodeId { get; set; }
        public Guid? PurchaseRequestId { get; set; }
        public int StatusId { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public int RequestTypeId { get; set; }
        public string? ReviewerBy { get; set; }
        public int DurationMonths { get; set; }
        public decimal Price { get; set; }
        public int MHPSeats { get; set; }
        public int OperationalSeats { get; set; }
        public int AdministrationalSeats { get; set; }
        public int Duration { get; set; }
        public string RejectionReason { get; set; }
        public int LicenseTypeId { get; set; }
        [NotMapped]
        public string Culture { get; set; }
    }
}

