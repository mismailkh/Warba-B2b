using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WB.Shared.Dtos.SiteManagement.RequestDtos
{
    public class SiteStatusRequestDto
    {
        [Key]
        public Guid Id { get; set; }
        public int StatusId { get; set; }
        public string ModifiedBy { get; set; } = string.Empty;
        [NotMapped]
        public string SiteName { get; set; }
        [NotMapped]
        public string Culture { get; set; }
    }
}
