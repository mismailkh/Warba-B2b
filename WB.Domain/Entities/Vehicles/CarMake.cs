using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WB.Domain.Common;

namespace WB.Domain.Entities.Vehicles
{
    [Table("CAR_MAKE", Schema = "ums")]
    public class CarMake : EntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string CarMakeEn { get; set; }
        public string CarMakeAr { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public ICollection<CarModel>? CarModels { get; set; } = new List<CarModel>();
    }
}
