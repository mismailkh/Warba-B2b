using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WB.Domain.Common;


namespace WB.Domain.Entities.Ums
{
    [Table("GROUP_CLAIMS", Schema = "ums")]
    public class GroupClaims : EntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid GroupId { get; set; }
        public int ClaimId { get; set; }
        public Claim Claim { get; set; }

    }

}
