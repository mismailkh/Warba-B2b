namespace WB.Shared.Dtos.Organization.ResponseDtos
{
    public class OrganizationsListResponseDto
    {
        public Guid Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string TypeName { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
