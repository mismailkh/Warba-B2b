using WB.Domain.Common;

namespace WB.Domain.Entities.Organization
{
    public class Designation : EntityBase
    {
        public Guid Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
    }
}
