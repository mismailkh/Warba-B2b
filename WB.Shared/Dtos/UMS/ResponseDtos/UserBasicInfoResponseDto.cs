using System.ComponentModel.DataAnnotations;

namespace WB.Shared.Dtos.UMS.ResponseDtos
{
    public class UserBasicInfoResponseDto
    {
        [Key]
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public int ContactTypeId { get; set; }
        public string ContactTypeName { get; set; }
        public string ContactNumber { get; set; }
        public bool IsPrimary { get; set; }
    }
}
