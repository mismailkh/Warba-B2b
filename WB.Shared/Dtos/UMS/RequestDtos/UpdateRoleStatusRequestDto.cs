namespace WB.Shared.Dtos.UMS.RequestDtos
{
    public class UpdateRoleStatusRequestDto
    {
        public string RoleId { get; set; }
        public bool IsActive { get; set; }
        public string ModifiedBy { get; set; }
    }
}
