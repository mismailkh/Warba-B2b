using System.ComponentModel.DataAnnotations;

namespace WB.Shared.Dtos.SiteManagement.ResponseDtos
{
    public class SiteListResponseDto
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ContactPerson { get; set; } = string.Empty;
        public string ContactNumber { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public string RegionName { get; set; } = string.Empty;
        public string CountryName { get; set; } = string.Empty;
        public string StateName { get; set; } = string.Empty;
        public string CityName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int GracePeriod { get; set; }
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public int LicenseTypeId { get; set; }
        public string LicenseTypeName { get; set; }
        public DateTime? LicenseStartDate { get; set; }
        public DateTime? LicenseExpiryDate { get; set; }
        public int SystemAdminCount { get; set; }
        public int MHPNumberCount { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class SiteLicenseHistoryResponseDto
    {
        public Guid Id { get; set; }
        public string OldLicenseName { get; set; } = string.Empty;
        public DateTime? OldLicenseStartDate { get; set; }
        public DateTime? OldLicenseExpiryDate { get; set; }
        public string OldLicenseStatusName { get; set; } = string.Empty;
    }
}
