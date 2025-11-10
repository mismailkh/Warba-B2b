using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WB.Domain.Common;

namespace WB.Domain.Entities.LOB
{
    [Table("PROCESS_SUBPROCESS_PR", Schema = "lob")]
    public class ProcessSubprocess : EntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ProcessId { get; set; }
        public int SubprocessId { get; set; }
        public Process Process { get; set; }
        public Subprocess Subprocess { get; set; }
    }
}
