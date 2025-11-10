using WB.Shared.Dtos.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WB.Shared.Dtos.UMS.ResponseDtos
{
    public class GroupDetailResponseDto : EntityBaseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<UserGroupResponseDto> UserGroup { get; set; }
        public List<GroupClaimsResponseDto> GroupClaims { get; set; }
    }

    public class UserGroupResponseDto
    {
        public Guid GroupId { get; set; }
        public string UserId { get; set; }
    }

    public class GroupClaimsResponseDto
    {
        public int Id { get; set; }
        public Guid GroupId { get; set; }
        public int ClaimId { get; set; }
    }
}
