using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class ReservationCleanerService : IReservationCleanerService
    {
        private readonly IBicycleRentalDbContext _dbContext;
        private readonly IPaymentGatewayService _paymentGateway;
        private readonly IAddressRepository _addressRepository;
        private readonly IReservationRepository _reservationRepository;

        public ReservationCleanerService(IBicycleRentalDbContext dbContext, IPaymentGatewayService paymentGateway, IAddressRepository addressRepository, IReservationRepository reservationRepository)
        {
            _dbContext = dbContext;
            _paymentGateway = paymentGateway;
            _addressRepository = addressRepository;
            _reservationRepository = reservationRepository;
        }

        public async Task CleanupExpiredReservationsAsync()
        {
            var expirationThreshold = DateTime.UtcNow.AddMinutes(-10);

            var expiredReservations = await _dbContext.Reservations
                .Include(r => r.Payment)
                .Include(r => r.Address)
                .Where(r => r.CreatedAt < expirationThreshold
                    && r.Status != ReservationStatus.Cancelled
                    && r.Status != ReservationStatus.Confirmed
                    && (r.Payment.Status == PaymentStatus.Pending || r.Payment.Status == null))
                .ToListAsync();

            foreach (var reservation in expiredReservations)
            {
                if (reservation.Payment.Status == null)
                {
                    if (reservation.Address.Type != AddressType.PickupPoint)
                    {
                        await _addressRepository.DeleteAsync(reservation.Address);
                    }
                    reservation.Status = ReservationStatus.Cancelled;
                }
                else if (reservation.Payment.Status == PaymentStatus.Pending)
                {
                    await _paymentGateway.CancelTransactionAsync(reservation.Payment.PayuOrderId);
                    reservation.Status = ReservationStatus.Cancelled;
                }
                else
                {
                    throw new InvalidOperationException($"Cannot cancel reservation with status {reservation.Payment.Status} id: {reservation.Id}.");
                }
            }
            await _dbContext.SaveChangesAsync();
        }
    }
}

