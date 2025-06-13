using MediatR;

namespace Application.CQRS.Payment.Command.Refund
{
    public class RefundCommand : IRequest
    {
        public Guid ReservationId { get; set; } = default!;
        public string PayUOrderId { get; set; } = default!;
        public string Description { get; set; } = default!;
    }
}
