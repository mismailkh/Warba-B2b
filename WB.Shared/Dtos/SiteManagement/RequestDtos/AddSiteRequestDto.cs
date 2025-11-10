using System.ComponentModel.DataAnnotations.Schema;

namespace WB.Shared.Dtos.SiteManagement.RequestDtos
{
    public class AddSiteRequestDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ContactPerson { get; set; } = string.Empty;
        public string CountryCode { get; set; } = string.Empty;
        public string ContactNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Website { get; set; } = string.Empty;
        public int RegionId { get; set; }
        public int CategoryId { get; set; }
        public int CountryId { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }
        public string? Zipcode { get; set; }
        public string Address { get; set; }
        public int GracePeriod { get; set; }
        public int StatusId { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public string? ModifiedBy { get; set; }
        public string? DeletedBy { get; set; }
        public int HealthBodyId { get; set; }
        public string SiteLogo { get; set; }
        public AssignSiteLicenseRequestDto? AssignSiteLicenses { get; set; } = new AssignSiteLicenseRequestDto();
        public PaymentRequestDto LicensePayments { get; set; } = new PaymentRequestDto();
    }

    public class AssignSiteLicenseRequestDto
    {
        public Guid Id { get; set; }
        public Guid SiteId { get; set; }
        public DateTime ExpiryDate { get; set; }        
        public int StatusId { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public int MHPSeats { get; set; }
        public int OperationalSeats { get; set; }
        public int AdministrationalSeats { get; set; }
        public int Duration { get; set; }
        public int LicenseTypeId { get; set; }
    }
    public class PaymentRequestDto
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public string Procedure { get; set; } = string.Empty;
        public DateTime PaymentDate { get; set; }
        public int StatusId { get; set; }
        public int PaymentMethodId { get; set; }
        public Guid? PurchaseRequestId { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
    }
}
