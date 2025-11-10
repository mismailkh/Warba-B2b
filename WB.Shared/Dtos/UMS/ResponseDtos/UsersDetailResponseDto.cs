using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WB.Shared.Dtos.UMS.ResponseDtos
{
    public class UsersDetailResponseDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Nationality { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string? Avatar { get; set; }
    }
}
