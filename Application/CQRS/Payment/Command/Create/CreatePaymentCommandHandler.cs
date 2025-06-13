using Application.DTO.Payment;
using Domain.Interfaces;
using MediatR;

namespace Application.CQRS.Payment.Command.Create
{
    public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, OrderResponse>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IPaymentGatewayService _paymentGatewayService;

        public CreatePaymentCommandHandler(IPaymentRepository paymentRepository, IPaymentGatewayService paymentGatewayService)
        {
            _paymentRepository = paymentRepository;
            _paymentGatewayService = paymentGatewayService;
        }

        public async Task<OrderResponse> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            var orderResponse = await _paymentGatewayService.CreateOrderAsync(request);
            if (orderResponse != null)
            {
                var newPayment = new Domain.Entities.Payment()
                {
                    ReservationId = request.ReservationId,
                    PayuOrderId = orderResponse.OrderId,
                    RedirectUrl = orderResponse.RedirectUri,
                    Amount = request.TotalAmount
                };
                await _paymentRepository.CreateAsync(newPayment);
                return orderResponse;
            }
            else
            {
                throw new InvalidOperationException("Failed to create payment order. Please try again later.");
            }


        }

    }
}
