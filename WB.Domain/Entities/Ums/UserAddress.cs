using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WB.Domain.Common;
using WB.Domain.Entities.Lookups;

namespace WB.Domain.Entities.Ums
{
    [Table("USER_ADDRESS", Schema = "ums")]
    public class UserAddress : EntityBase
    {
        [Key]
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string? Address { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public User User { get; set; }
    }
}
