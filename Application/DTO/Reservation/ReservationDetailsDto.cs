using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.Bicycle;
using Application.DTO.Payment;
using Domain.Entities;

namespace Application.DTO.Reservation
{
    public class ReservationDetailsDto
    {
        public Guid Id { get; set; }
        public decimal TotalPrice { get; set; }
        public bool IsConfirmed { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public string DeliveryHours { get; set; } = default!;
        public Guid GuestId { get; set; }
        public Guid AddressId { get; set; }
        public List<BicycleDetailsDto> Bicycles { get; set; } = new();
        public PaymentDto Payment { get; set; } = default!;
    }
}
