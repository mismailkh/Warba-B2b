using WB.Shared.Dtos.General;

namespace WB.Shared.Dtos.Organization.RequestDtos
{
    public class SaveDesignationRequestDto
    {
        public Guid? Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public string LoggedInUser { get; set; }
    }
}
