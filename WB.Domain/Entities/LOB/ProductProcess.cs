using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WB.Domain.Common;

namespace WB.Domain.Entities.LOB
{
    [Table("PRODUCT_PROCESS_PR", Schema = "lob")]
    public class ProductProcess : EntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int ProcessId { get; set; }
        public Product Product { get; set; }
        public Process Process { get; set; }
        public ICollection<ProductProcessSubprocess> ProductProcessSubprocesses { get; set; }
    }
}
