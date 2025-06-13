namespace Domain.Entities
{
    public class Reservation
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public decimal TotalPrice { get; set; }
        public ReservationStatus Status { get; set; } = ReservationStatus.Pending;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string DeliveryHours { get; set; } = default!;
        public Guid GuestId { get; set; }
        public Guest Guest { get; set; } = default!;
        public Guid AddressId { get; set; }
        public Address Address { get; set; } = default!;
        public ICollection<Bicycle> Bicycles { get; set; } = default!;
        public Payment Payment { get; set; } = default!;
    }
    public enum ReservationStatus
    {
        Pending,
        Confirmed,
        Cancelled,
    }
}
