using Domain.Interfaces;
using MediatR;

namespace Application.CQRS.Payment.Command.Refund
{
    public class RefundCommandHandler : IRequestHandler<RefundCommand>
    {
        private readonly IPaymentGatewayService _paymentGatewayService;
        private readonly IReservationRepository _reservationRepository;

        public RefundCommandHandler(IPaymentGatewayService paymentGatewayService, IReservationRepository reservationRepository)
        {
            _paymentGatewayService = paymentGatewayService;
            _reservationRepository = reservationRepository;
        }

        public async Task Handle(RefundCommand request, CancellationToken cancellationToken)
        {
            await _paymentGatewayService.RefundTransactionAsync(request.PayUOrderId, request.Description);
            var reservation = await _reservationRepository.GetByIdAsync(request.ReservationId);
            if (reservation == null)
            {
                throw new ArgumentNullException(nameof(reservation));
            }

            reservation.Status = Domain.Entities.ReservationStatus.Cancelled;
            await _reservationRepository.UpdateAsync(reservation);
        }
    }
}
