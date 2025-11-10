namespace WB.Shared.Dtos.Organization.ResponseDtos
{
    public class DepartmentDetailResponseDto
    {
        public Guid Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public Guid OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        public string OrganizationTypeName { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
