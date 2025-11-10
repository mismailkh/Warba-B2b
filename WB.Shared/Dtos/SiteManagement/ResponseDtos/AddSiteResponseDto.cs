namespace WB.Shared.Dtos.SiteManagement.ResponseDtos
{
    public class AddSiteResponseDto
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
        public AssignSiteLicenseResponseDto? AssignSiteLicenses { get; set; } = new AssignSiteLicenseResponseDto();
    }
    public class AssignSiteLicenseResponseDto
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
}
