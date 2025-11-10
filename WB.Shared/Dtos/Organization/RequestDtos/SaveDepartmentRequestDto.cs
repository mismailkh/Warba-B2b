namespace WB.Shared.Dtos.Organization.RequestDtos
{
    public class SaveDepartmentRequestDto
    {
        public Guid? Id { get; set; }
        public Guid OrganizationId { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public string LoggedInUser { get; set; }
    }
}
