using Microsoft.AspNetCore.Identity;

namespace WB.Domain.Entities.Ums
{
    public class User : IdentityUser<string>
    {
        public bool IsActive { get; set; } = true;
        public bool AllowAccess { get; set; } = true;
        public bool IsLocked { get; set; } = false;
        public bool IsPasswordReset { get; set; } = false;
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool IsDeleted { get; set; }
        public UserPersonalInformation PersonalInformation { get; set; }
        public ICollection<UserClaims> UserClaims { get; set; } = new List<UserClaims>();
        public ICollection<UserRoles> UserRoles { get; set; } = new List<UserRoles>();
    }
}
