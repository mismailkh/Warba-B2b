using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WB.Domain.Common;
using WB.Domain.Entities.Vehicles;

namespace WB.Domain.Entities.ProductPackages
{
    [Table("PRODUCT_PACKAGE", Schema = "ums")]
    public class ProductPackage : EntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public List<CarMake> CarMake { get; set; } = new List<CarMake>();
        public bool IsActive { get; set; }
        public int AgeOfInsured { get; set; }
        public int PolicyDuration { get; set; }
        public int YearMakeFrom { get; set; }
        public int YearMakeTo { get; set; }
        public decimal SiMin { get; set; }
        public decimal SiMax { get; set; }
        public int RiskRateMin { get; set; }
        public int RiskRateMax { get; set; }
        public int Retention { get; set; }
        public decimal MinPremium { get; set; }
        public int Commission { get; set; }
        public decimal InsuranceFee { get; set; }
        public string TandC_En { get; set; }
        public string TandC_Ar { get; set; }
    }
}
