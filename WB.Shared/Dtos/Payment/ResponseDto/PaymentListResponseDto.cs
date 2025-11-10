using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WB.Shared.Dtos.General;
using WB.Shared.Enums;

namespace WB.Shared.Dtos.Payment.ResponseDto
{
    public class PaymentListResponseDto : EntityBaseDto
    {
        public Guid Id { get; set; }
        public DateTime PaymentDate { get; set; }
        public string? PaymentMadeById { get; set; }
        public string? PaymentMadeByName { get; set; }
        public int PaymentPurposeId { get; set; }
        public decimal Amount { get; set; }
        public int PaymentMethodId { get; set; }
        public int StatusId { get; set; }
    }
}
