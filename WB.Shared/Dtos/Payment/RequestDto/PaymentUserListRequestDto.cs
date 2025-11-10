using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WB.Shared.Dtos.Payment.RequestDto
{
    public class PaymentUserListRequestDto
    {
        public string UserId { get; set; }
        public string FullName { get; set; }
    }
}
