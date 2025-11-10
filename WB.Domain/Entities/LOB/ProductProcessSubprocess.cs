using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WB.Domain.Common;

namespace WB.Domain.Entities.LOB
{
    [Table("PRODUCT_PROCESS_SUBPROCESS_PR", Schema = "lob")]
    public class ProductProcessSubprocess : EntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ProductProcessId { get; set; }
        public int ProcessSubprocessId { get; set; }
        public ProductProcess ProductProcess { get; set; }
        public ProcessSubprocess ProcessSubprocess { get; set; }
    }
}
