using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WB.Domain.Common;

namespace WB.Domain.Entities.AuditLogs
{
    [Table("ERROR_LOG", Schema = "app")]
    public class ErrorLog
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime LogDate { get; set; }
        public string? Source { get; set; }
        public string Module { get; set; }
        public string Computer {  get; set; }
        public string? CreatedBy { get; set; }
        public string? TerminalId { get; set; }
        public string? IPAddress { get; set; }
        public string? Operation { get; set; }
        public string Message { get; set; }
        public string? StackTrace { get; set; }
    }
}
