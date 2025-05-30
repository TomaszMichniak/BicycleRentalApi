using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.Payment;
using MediatR;

namespace Application.CQRS.Payment.Command.Create
{
    public class CreatePaymentCommand: IRequest<OrderResponse>
    {
        public string CustomerIp { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal TotalAmount { get; set; } = default!;
        public Guid ReservationId { get; set; }
        public BuyerDto Buyer { get; set; } = default!;
        public List<ProductDto> Products { get; set; } = default!;
    }
}
