namespace WB.Shared.Dtos.UMS.ResponseDtos
{
    public class CountryWiseLicenseMetadataPricesResponseDto
    {
        public decimal MHPSeatCost { get; set; }
        public decimal AdministrationalSeatCost { get; set; }
        public decimal OperationalSeatCost { get; set; }
        public decimal SessionCost { get; set; }
    }
}
