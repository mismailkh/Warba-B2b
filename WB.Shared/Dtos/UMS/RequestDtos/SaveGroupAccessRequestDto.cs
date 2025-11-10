
using WB.Shared.Dtos.UMS.ResponseDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WB.Shared.Dtos.UMS.RequestDtos
{
    public class SaveGroupAccessRequestDto
    {
        public Guid GroupId {  get; set; }
        public string CreatedBy {  get; set; }
        public IList<ClaimListResponseDto> ClaimsList { get; set; } = new List<ClaimListResponseDto>();
        public IList<UserListResponseDto> UsersList { get; set; } = new List<UserListResponseDto>();
    }
}
