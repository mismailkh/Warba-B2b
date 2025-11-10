using System.ComponentModel.DataAnnotations.Schema;

namespace WB.Shared.Dtos.SiteManagement.RequestDtos
{
    public class SiteDeactivationReasonRequestDto
    {
        public int Id { get; set; }
        public Guid SiteId { get; set; }
        public string Reason { get; set; }
        public int DeactivationTypeId { get; set; }
        public string CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public string? DeletedBy { get; set; }
        [NotMapped]
        public string SiteName { get; set; }
        [NotMapped]
        public string Culture { get; set; }
    }
}
