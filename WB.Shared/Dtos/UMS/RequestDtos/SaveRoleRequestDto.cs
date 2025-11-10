using WB.Shared.Dtos.General;

namespace WB.Shared.Dtos.UMS.RequestDtos
{
    public class SaveRoleRequestDto : EntityBaseDto
    {
        public string? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
