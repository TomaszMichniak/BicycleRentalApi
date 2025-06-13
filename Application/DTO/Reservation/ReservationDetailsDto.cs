using Application.DTO.Address;
using Application.DTO.Bicycle;
using Application.DTO.Guest;
using Application.DTO.Payment;

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
        public GuestDetailsDto Guest { get; set; } = default!;
        public AddressDetailsDto Address { get; set; } = default!;
        public List<BicycleDetailsDto> Bicycles { get; set; } = new();
        public PaymentDto Payment { get; set; } = default!;
    }
}
