using Microsoft.AspNetCore.Identity;
using WB.Domain.Entities.Ums;
using System.ComponentModel.DataAnnotations;

namespace WB.Domain.Entities.Ums
{
    public class Role : IdentityRole<string>
    {
        [Key]
        public string Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string? DescriptionEn { get; set; }
        public string? DescriptionAr { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public ICollection<UserRoles> UserRole { get; set; }
    }
}
