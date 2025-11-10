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
    [Table("CAR_MODEL", Schema = "ums")]
    public class CarModel : EntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string CarModelEn { get; set; }
        public string CarModelAr { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
