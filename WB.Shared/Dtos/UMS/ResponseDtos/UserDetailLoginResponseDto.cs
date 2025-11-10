namespace WB.Shared.Dtos.UMS.ResponseDtos
{
    public class UserDetailLoginResponseDto
    {
        public string UserId { get; set; }
        public Guid? SiteId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public IList<string> UserRoles { get; set; }
        public string SecurityStamp { get; set; }
        public string? FullNameEn { get; set; }
        public string? FullNameAr { get; set; }
        public bool IsPasswordReset { get; set; }
        public string EmployeeId { get; set; }
        public string CivilId { get; set; }
        public Guid? OrganizationId { get; set; }
        public Guid? DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
    }
}
