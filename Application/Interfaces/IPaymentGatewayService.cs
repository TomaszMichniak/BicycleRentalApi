using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.CQRS.Payment.Command.Create;
using Application.DTO.Payment;

namespace Domain.Interfaces
{
    public interface IPaymentGatewayService
    {
        Task CancelTransactionAsync(string payuOrderId);
        public Task<OrderResponse> CreateOrderAsync(CreatePaymentCommand request);
    }
}
