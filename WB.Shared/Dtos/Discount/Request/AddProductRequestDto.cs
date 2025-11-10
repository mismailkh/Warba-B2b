using WB.Shared.Dtos.General;

namespace WB.Shared.Dtos.Discount.Request
{
    public class AddProductRequestDto : EntityBaseDto
    {
        public int Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public bool IsActive { get; set; }
    }
}
