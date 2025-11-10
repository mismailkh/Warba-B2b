using static WB.Shared.Enums.GeneralEnums;

namespace WB.Shared.Dtos.General.RequestDtos
{
    public class TermsAndConditionsRequestDto
    {
        public int TypeId { get; set; }
        public string? Content { get; set; }
        public string? CreatedBy { get; set; }
    }
}
