namespace WB.Shared.Dtos.UMS.RequestDtos
{
    public class SaveRoleClaimRequestDto
    {
        public string RoleId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
        public string CreatedBy { get; set; }
    }
}
