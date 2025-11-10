using Microsoft.AspNetCore.Identity;


namespace WB.Domain.Entities.Ums
{
    public class UserClaims : IdentityUserClaim<string>
    {
        public User User { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
