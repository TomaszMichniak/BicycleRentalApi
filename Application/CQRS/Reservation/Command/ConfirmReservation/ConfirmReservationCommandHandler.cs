using Domain.Interfaces;
using MediatR;

namespace Application.CQRS.Reservation.Command.ConfirmReservation
{
    public class ConfirmReservationCommandHandler : IRequestHandler<ConfirmReservationCommand>
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IEmailService _emailService;

        public ConfirmReservationCommandHandler(IReservationRepository reservationRepository, IEmailService emailService)
        {
            _reservationRepository = reservationRepository;
            _emailService = emailService;
        }

        public async Task Handle(ConfirmReservationCommand request, CancellationToken cancellationToken)
        {
            var reservation = await _reservationRepository.GetWithDetailsByIdAsync(request.Id);
            if (reservation == null)
            {
                throw new Exception();
            }
            reservation.Status = Domain.Entities.ReservationStatus.Confirmed;
            await _reservationRepository.UpdateAsync(reservation);

            var subject = "Twoja rezerwacja została opłacona";
            var bodyHtml = await _emailService.GetReservationConfirmationHTML(reservation);
            await _emailService.SendEmailAsync(reservation.Guest.Email, subject, bodyHtml);


        }
    }
}
