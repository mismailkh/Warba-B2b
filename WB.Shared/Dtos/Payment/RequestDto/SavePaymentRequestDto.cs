using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WB.Shared.Dtos.General;

namespace WB.Shared.Dtos.Payment.RequestDto
{
    public class SavePaymentRequestDto : EntityBaseDto
    {
        public Guid Id { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMadeById { get; set; }
        public int? PaymentPurposeId { get; set; } 
        public decimal Amount { get; set; }
        public int PaymentMethodId { get; set; }
        public int StatusId { get; set; }
    }
}
