using System.ComponentModel.DataAnnotations.Schema;
using WB.Shared.Dtos.UMS.Others;

namespace WB.Shared.Dtos.UMS.ResponseDtos
{
    public class ListProcessLogResponseDto
    {
        public Guid Id { get; set; } 
        public string? Process { get; set; } 
        //public string? Module { get; set; }
        public string Description { get; set; }
        //public string Computer { get; set; }
        public string? TerminalId { get; set; }
        public string? IPAddress { get; set; }
        public string? CreatedBy { get; set; }
        [NotMapped]
        public int? TotalCount { get; set; }

        public string UserId { get; set; }
        public TimeOnly TimeOnly { get; set; } // done
    }
}
