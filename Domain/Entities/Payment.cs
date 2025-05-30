using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Payment
    {

        public Guid Id { get; set; }= Guid.NewGuid();
        public Guid ReservationId { get; set; }
        public Reservation Reservation { get; set; } = default!;
        public string PayuOrderId { get; set; }=default!;
        public PaymentStatus? Status { get; set; }
        public string RedirectUrl { get; set; } = default!;
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "PLN";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? PaidAt { get; set; }
    }
    public enum PaymentStatus
    {
        Pending, 
        WaitingForConfirmation,
        Paid,      
        Failed,    
        Refunded,  
        Cancelled 

    }
}
