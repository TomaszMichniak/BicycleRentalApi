using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Payment
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ReservationId { get; set; }
        public Reservation Reservation { get; set; } = default!;
        public decimal Amount { get; set; }
        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? PaidAt { get; set; }
        public string? TransactionId { get; set; }
        public string? Token { get; set; }
        public string Method { get; set; } = "P24";
    }
    public enum PaymentStatus
    {
        Pending,   // Utworzona, ale jeszcze nieopłacona
        Paid,      // Opłacona (potwierdzona przez webhook P24)
        Failed,    // Błąd lub odrzucona
        Refunded,  // Zwrot
        Cancelled  // Anulowana ręcznie lub przez system
    }
}
