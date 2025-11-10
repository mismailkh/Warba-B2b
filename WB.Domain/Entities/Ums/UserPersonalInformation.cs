using WB.Domain.Common;
using WB.Domain.Entities.Lookups;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WB.Domain.Entities.Ums
{
    [Table("USER_PERSONAL_INFORMATION", Schema = "ums")]
    public class UserPersonalInformation : EntityBase
    {
        [Key]
        public string UserId { get; set; }
        public string FirstNameEn { get; set; }
        public string FirstNameAr { get; set; }
        public string? SecondNameEn { get; set; }
        public string? SecondNameAr { get; set; }
        public string LastNameEn { get; set; }
        public string LastNameAr { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Guid? AuthorizationLevelId { get; set; }
        public Guid? DepartmentId { get; set; }
        public Guid? DesignationId { get; set; }
        public string? CivilId { get; set; }
        public DateTime? CivilIdExpiryDate { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Avatar { get; set; }
        public User User { get; set; }
    }
}
