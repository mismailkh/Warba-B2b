using WB.Domain.Common;

namespace WB.Domain.Entities.Organization
{
    public class OrganizationPaymentMethod : EntityBase
    {
        public Guid Id { get; set; }
        public int MethodId { get; set; }
        public string Code { get; set; }
        public Guid OrganizationId { get; set; }
        public virtual Organization Organization { get; set; }
    }
}