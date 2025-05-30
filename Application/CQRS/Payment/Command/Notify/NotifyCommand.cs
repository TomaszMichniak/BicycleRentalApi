using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MediatR;

namespace Application.CQRS.Payment.Command.Notify
{
    public class NotifyCommand : IRequest<bool>
    {
        public string RawBody { get; set; } = default!;
        public string SignatureHeader { get; set; } = default!;
     
    }
    public class PayuOrder
    {
        [JsonPropertyName("orderId")]
        public string OrderId { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
    }
}
