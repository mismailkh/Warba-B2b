using Microsoft.AspNetCore.Identity;
using WB.Domain.Entities.Ums;

namespace WB.Domain.Entities.Ums
{
    public class UserRoles : IdentityUserRole<string>
    {
        public User User { get; set; }
        public Role Role { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
