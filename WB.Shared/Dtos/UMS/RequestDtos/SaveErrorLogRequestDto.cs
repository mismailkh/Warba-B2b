using System.ComponentModel.DataAnnotations.Schema;

namespace WB.Shared.Dtos.UMS.RequestDtos
{
    public class SaveErrorLogRequestDto
    {
        public Guid? Id { get; set; }
        public DateTime LogDate { get; set; }
        public string? Source { get; set; }
        public string? Module { get; set; }
        public string? Computer { get; set; }
        public string? Username { get; set; }
        public string? TerminalId { get; set; }
        public string? IPAddress { get; set; }
        public string? Operation { get; set; }
        public string? Message { get; set; }
        public string? StackTrace { get; set; }
        [NotMapped]
        public string? Token { get; set; }
    }
}
