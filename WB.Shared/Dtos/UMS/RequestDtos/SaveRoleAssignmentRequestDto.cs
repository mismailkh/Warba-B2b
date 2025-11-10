using WB.Shared.Dtos.UMS.ResponseDtos;

namespace WB.Shared.Dtos.UMS.RequestDtos
{
    public class SaveRoleAssignmentRequestDto
    {
        public string? RoleId { get; set; }
        public string? CreatedBy { get; set; }
        public string? RoleName { get; set; } 
        
        public string? Culture { get; set; }
        public IList<UserListResponseDto> UsersList { get; set; } = new List<UserListResponseDto>();

    }
}
