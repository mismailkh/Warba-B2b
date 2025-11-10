namespace WB.Shared.Dtos.Organization.RequestDtos
{
    public class SaveOrganizationRequestDto
    {
        public Guid? Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string? Description { get; set; }
        public string BrokerageCode { get; set; }
        public int TypeId { get; set; }
        public bool IsDirectPurchaseAllowed { get; set; }
        public string? Logo { get; set; }
        public bool IsActive { get; set; }
        public string LoggedInUserId { get; set; }
        public List<SaveOrganizationPaymentMethodRequestDto> PaymentMethods { get; set; } = new();
    }
    public class SaveOrganizationPaymentMethodRequestDto
    {
        public int MethodId { get; set; }
        public string Code { get; set; }
        public string? MethodName { get; set; }
    }
}
