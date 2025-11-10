using WB.Domain.Common;

namespace WB.Domain.Entities.Organization
{
    public class Organization : EntityBase
    {
        public Guid Id { get; set; }
        public int TypeId { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string? Description { get; set; }
        public string? Logo { get; set; }
        public string BrokerageCode { get; set; }
        public bool IsDirectPurchaseAllowed { get; set; }
        public bool IsActive { get; set; }
        public OrganizationType OrganizationType { get; set; }
        public virtual ICollection<OrganizationPaymentMethod> OrganizationPaymentMethods { get; set; }
        public ICollection<Department> Departments { get; set; }
    }
}
