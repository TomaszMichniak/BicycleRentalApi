using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.DTO.Payment
{
    public class OrderResponse
    {
        [JsonPropertyName("orderId")]
        public string OrderId { get; set; } = default!;

        [JsonPropertyName("redirectUri")]
        public string RedirectUri { get; set; } = default!;
        [JsonPropertyName("status")]
        public Status Status { get; set; } = default!;

        //public List<PayMethod>? PayMethods { get; set; }
    }
    public class Status{
        [JsonPropertyName("statusCode")]
        public string StatusCode { get; set; } =default!;
    }
}
