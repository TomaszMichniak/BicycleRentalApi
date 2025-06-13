using Application.CQRS.Payment.Command.Create;
using Application.DTO.Payment;

namespace Domain.Interfaces
{
    public interface IPaymentGatewayService
    {
        Task CancelTransactionAsync(string payuOrderId);
        public Task<OrderResponse> CreateOrderAsync(CreatePaymentCommand request);
        public Task RefundTransactionAsync(string payuOrderId, string description);
    }
}
