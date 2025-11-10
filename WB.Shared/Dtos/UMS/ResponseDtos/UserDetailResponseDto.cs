using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WB.Shared.Dtos.UMS.ResponseDtos
{
    public class UserDetailResponseDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public bool IsLocked { get; set; }
        public UserPersonalInformationResponseDto PersonalInformation { get; set; } = new();
        public IList<UserContactResponseDto> Contacts { get; set; } = new List<UserContactResponseDto>();
        public IList<UserAddressResponseDto> Addresses { get; set; } = new List<UserAddressResponseDto>();
        public IList<UserGroupResponseDto> UserGroups { get; set; } 
        public IList<UserClaimResponseDto> UserClaims { get; set; }
    }
    public class UserPersonalInformationResponseDto
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string FullName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int NationalityId { get; set; }
        public string? NationalityName { get; set; }
        public int GenderId { get; set; } 
        public string? GenderName { get; set; }
        public string? CivilId { get; set; }
        public string? PassportNumber { get; set; }
        public string? Avatar { get; set; }
    }
    public class UserContactResponseDto
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public int ContactTypeId { get; set; }
        public string ContactNumber { get; set; }
        public bool IsPrimary { get; set; }
    }
    public class UserAddressResponseDto
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public int StateId { get; set; }
        public string StateName { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public string Address { get; set; }
    }
    public class UserClaimResponseDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
    }
    public class UserAssignedRoleResponseDto
    {
        public string? RoleId { get; set; }
    }
}
