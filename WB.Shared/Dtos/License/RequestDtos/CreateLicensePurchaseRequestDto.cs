using System.ComponentModel.DataAnnotations.Schema;

namespace WB.Shared.Dtos.License.RequestDtos
{
    public class CreateLicensePurchaseRequestDto
    {
        public Guid SiteId { get; set; }
        public int LicenseTypeId { get; set; }
        public int MHPSeats { get; set; }
        public int OperationalSeats { get; set; }
        public int AdministrationalSeats { get; set; }
        public int Duration { get; set; } = 1;
        public decimal Amount { get; set; }
        public string? Name { get; set; }
        public int RequestTypeId { get; set; }
        public int StatusId { get; set; }
        public string CreatedBy { get; set; } = default!;
        public string? ModifiedBy { get; set; }
    }
}