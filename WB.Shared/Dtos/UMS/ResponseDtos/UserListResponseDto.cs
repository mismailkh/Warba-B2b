using System.ComponentModel.DataAnnotations.Schema;

namespace WB.Shared.Dtos.UMS.ResponseDtos
{
    public record UserListResponseDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? FullName { get; set; }
        public bool IsPasswordReset { get; set; }
        public bool IsLocked { get; set; }
        public int? GenderId { get; set; }
        public string? Gender { get; set; }
        public int? NationalityId { get; set; }
        public string? Nationality { get; set; }
        public bool IsActive { get; set; }
        public string? Avatar { get; set; }
        public bool IsSuperAdmin { get; set; }
        [NotMapped]
        public IList<string>? UserRoles { get; set; }
        [NotMapped]
        public string? EmployeeId { get; set; }
        [NotMapped]
        public string? CivilId { get; set; }
        [NotMapped]
        public bool IsNotification { get; set; }=false;
    }

}
