using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WB.Shared.Dtos.ProductPackage.Response
{
    public class GetProductPackageResponseDto
    {
        public Guid Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
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
