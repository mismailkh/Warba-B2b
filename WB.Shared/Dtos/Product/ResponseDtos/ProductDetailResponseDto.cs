using System.ComponentModel.DataAnnotations;

namespace WB.Shared.Dtos.Product.ResponseDtos
{
    public class ProductDetailResponseDto
    {
        [Key]
        public int Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public List<ProductProcessListResponseDto> ProductProcesses { get; set; } = new List<ProductProcessListResponseDto>();
    }
    public class ProductProcessListResponseDto
    {
        [Key]
        public int Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public bool IsAssigned { get; set; }
        public List<ProductProcessSubprocessListResponseDto> ProductProcessSubprocesses { get; set; } = new List<ProductProcessSubprocessListResponseDto>();
    }
    public class ProductProcessSubprocessListResponseDto
    {
        [Key]
        public int Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public bool IsAssigned { get; set; }
    }
}
