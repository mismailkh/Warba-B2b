using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WB.Shared.Dtos.General.RequestDtos
{
    public class LookupRequestDto
    {
        public string Culture { get; set; }
        public List<LookupTableRequest> Tables { get; set; }
    }

    public class LookupTableRequest
    {
        public string Table { get; set; }
        public string? FilterColumn { get; set; } 
        public object? FilterValue { get; set; }
        public List<string> ExtraColumns { get; set; } = new();
    }
}
