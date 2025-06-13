using MediatR;

namespace Application.CQRS.Reservation.Command.ConfirmReservation
{
    public class ConfirmReservationCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
