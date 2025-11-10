using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WB.Shared.Dtos.General;

namespace WB.Shared.Dtos.ProductPackage.Request
{
    public class CreateProductPackageRequestDto : EntityBaseDto
    {
        public Guid? Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public List<CarMakeDto> CarMake { get; set; } = new List<CarMakeDto>();
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

    public class CarMakeDto : EntityBaseDto
    {
        public Guid Id { get; set; }
        public string CarMakeEn { get; set; }
        public string CarMakeAr { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public ICollection<CarModelDto> CarModels { get; set; } = new List<CarModelDto>();
    }

    public class CarModelDto : EntityBaseDto
    {
        public Guid Id { get; set; }
        public string CarModelEn { get; set; }
        public string CarModelAr { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
