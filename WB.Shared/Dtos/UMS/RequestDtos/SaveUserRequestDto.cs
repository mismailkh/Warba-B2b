using System.ComponentModel.DataAnnotations.Schema;

namespace WB.Shared.Dtos.UMS.RequestDtos
{
    public class SaveUserRequestDto
    {
        public string? Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? Password { get; set; }
        public string CreatedBy { get; set; }
        public Guid? SiteId { get; set; }
        public UserPersonalInformationRequestDto PersonalInformation { get; set; } = new UserPersonalInformationRequestDto();
        public IList<UserContactRequestDto> Contacts { get; set; } = new List<UserContactRequestDto>();
        public IList<UserAddressRequestDto> Addresses { get; set; } = new List<UserAddressRequestDto>();
        public UserRoleRequestDto Roles { get; set; } = new UserRoleRequestDto();
        [NotMapped]
        public string Culture { get; set; }
        [NotMapped]
        public string SiteName { get; set; }
    }

    public class UserPersonalInformationRequestDto
    {
        public string FullName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int DesignationId { get; set; }
        public int NationalityId { get; set; }
        public int GenderId { get; set; }
        public int DepartmentId { get; set; }
        public string? CivilId { get; set; }
        public string? PassportNumber { get; set; }
        public string? Avatar { get; set; }
    }
    public class UserContactRequestDto
    {
        public int ContactTypeId { get; set; }
        public string ContactNumber { get; set; }
        public bool IsPrimary { get; set; }
    }
    public class UserAddressRequestDto
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public int StateId { get; set; }
        public string StateName { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public string Address { get; set; }
    }

    public class CreateAdminRequestDto
    {
        public string Email { get; set; }
        public string? Password { get; set; }
        public string FirstNameEn { get; set; }
        public string FirstNameAr { get; set; }
        public string LastNameEn { get; set; }
        public string LastNameAr { get; set; }
    }

    public class UserRoleRequestDto
    {
        public string? UserId { get; set; }
        public string RoleId { get; set; }
    }
}
