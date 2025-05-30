using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.DTO.Payment
{
    public class ProductDto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = default!;

        [JsonPropertyName("unitPrice")]
        public string UnitPrice { get; set; } = default!; // grosze jako string

        [JsonPropertyName("quantity")]
        public string Quantity { get; set; } = default!;
    }
}
