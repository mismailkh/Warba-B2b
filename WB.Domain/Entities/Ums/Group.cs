using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WB.Domain.Common;

namespace WB.Domain.Entities.Ums
{
    [Table("GROUP", Schema = "ums")]
    public class Group : EntityBase
    {
        [Key]
        public Guid Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string DescriptionEn { get; set; }
        public string DescriptionAr { get; set; }
        public ICollection<UserGroup> UserGroup { get; set; }
        public ICollection<GroupClaims> GroupClaims { get; set; }
	}
}
