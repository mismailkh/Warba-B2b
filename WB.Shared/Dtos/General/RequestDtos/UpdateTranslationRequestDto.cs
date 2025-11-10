using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WB.Shared.Dtos.General.RequestDtos
{
    public class UpdateTranslationRequestDto
    {
        public string Key { get; set; }
        public string ValueEn { get; set; }
        public string ValueAr { get; set; }
    }
}
