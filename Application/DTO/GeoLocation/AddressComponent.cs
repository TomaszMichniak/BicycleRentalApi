using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.DTO.GeoLocation
{
    public class AddressComponent
    {
        [JsonPropertyName("long_name")]
        public string LongName { get; set; } = default!;

        [JsonPropertyName("short_name")]
        public string ShortName { get; set; } = default!;

        public List<string> Types { get; set; } = new();
    }
}
