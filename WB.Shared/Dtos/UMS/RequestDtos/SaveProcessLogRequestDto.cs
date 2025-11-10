using System.ComponentModel.DataAnnotations.Schema;

namespace WB.Shared.Dtos.UMS.RequestDtos
{
    public class SaveProcessLogRequestDto
    {
        public Guid? Id { get; set; }
        public string Process {  get; set; }
        public string Module { get; set; }
        public string? Description { get; set; }
        public string? Computer { get; set; }
        public string? Username { get; set; }
        public string? TerminalId { get; set; }
        public string? IPAddress { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime LogDate { get; set; }
        [NotMapped]
        public string? Token { get; set; }
    }
}
