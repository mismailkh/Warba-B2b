using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WB.Shared.Dtos.General.ResponseDtos
{
    public class TranslationUserCheckResponseDto
    {
        public int UserCount { get; set; }
        public List<TranslationResponseDto> Translations { get; set; }
    }
    public record TranslationResponseDto
    {
        public string Key { get; set; }
        public string ValueEn { get; set; }
        public string ValueAr { get; set; }
    }
}
