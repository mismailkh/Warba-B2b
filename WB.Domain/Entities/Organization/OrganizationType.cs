using WB.Domain.Common;

namespace WB.Domain.Entities.Organization
{
    public class OrganizationType : EntityBase
    {
        public int Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public bool IsActive { get; set; }
        public ICollection<Organization> Organizations { get; set; }
    }
}
