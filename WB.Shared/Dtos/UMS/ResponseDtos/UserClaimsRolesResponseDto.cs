namespace WB.Shared.Dtos.UMS.ResponseDtos
{
    public class UserClaimsRolesResponseDto
    {
        public List<ClaimSucessResponse> Claims { get; set; }
        public List<UserAssignedRoleResponseDto> Roles { get; set; }
    }
}
