using WB.Domain.Common;

namespace WB.Domain.Entities.Organization
{
    public class Department : EntityBase
    {
        public Guid Id { get; set; }
        public Guid OrganizationId { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public Organization Organization { get; set; }
    }
}
