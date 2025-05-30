using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.DTO.Payment
{
    public class PaymentDto
    {
        public Guid Id { get; set; }
        public Guid ReservationId { get; set; }
        public string PayuOrderId { get; set; } = default!;
        public PaymentStatus? Status { get; set; }
        public string RedirectUrl { get; set; } = default!;
        public decimal Amount { get; set; }
        public string Currency { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
        public DateTime? PaidAt { get; set; }
    }
}
