namespace WB.Shared.Dtos.Organization.ResponseDtos
{
    public class OrganizationDetailResponseDto
    {
        public Guid? Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string? Description { get; set; }
        public string Logo { get; set; }
        public string BrokerageCode { get; set; }
        public int TypeId { get; set; }
        public string TypeName { get; set; }
        public bool IsDirectPurchaseAllowed { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public List<OrganizationPaymentMethodsResponseDto> PaymentMethods { get; set; } = new List<OrganizationPaymentMethodsResponseDto>();
    }

    public class OrganizationPaymentMethodsResponseDto
    {
        public int MethodId { get; set; }
        public string Code { get; set; }
        public string MethodName { get; set; }
    }
}
