namespace WB.Shared.Dtos.General.ResponseDtos
{
    public class PromoCodeValidationResponseDto
    {
        public Guid? Id { get; set; }
        public int DiscountValue { get; set; }
        public int DiscountType { get; set; }
        public bool IsValid { get; set; } = true;
    }
}
