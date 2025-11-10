using WB.Shared.Dtos.General;

namespace WB.Shared.Dtos.UMS.ResponseDtos
{
    public class RoleDetailResponseDto : EntityBaseDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<UserRoleResponseDto> UserRole { get; set; }
    }
    public class UserRoleResponseDto
    {
        public string? RoleId { get; set; }
        public string? UserId { get; set; }
    }
}
