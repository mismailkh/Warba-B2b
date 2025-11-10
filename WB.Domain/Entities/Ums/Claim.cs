using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WB.Domain.Entities.Ums
{
    [Table("CLAIM_PR", Schema = "ums")]
    public class Claim
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string TitleEn { get; set; }
        public string TitleAr { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
        public bool IsDeleted { get; set; }
        public int ModuleId { get; set; }
    }
}
