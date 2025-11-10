using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WB.Shared.Dtos.PromoCode.RequestDtos;

namespace WB.Shared.Dtos.PromoCode.RequestDtos
{
    public class PromoCodeSiteListRequestDto
    {
        public Guid Id { get; set; }
        [NotMapped]
        public string? Name { get; set; }
        [NotMapped]
        public IEnumerable<Guid>? SelectedSiteIds { get; set; } = new List<Guid>();
        [NotMapped]
        public int RegionId { get; set; }
        [NotMapped]
        public int CountryId { get; set; }
        [NotMapped]
        public int StateId { get; set; }
        [NotMapped]
        public int CityId { get; set; }
    }
}
