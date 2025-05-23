using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.GeoLocation
{
    public class GoogleGeocodeResponse
    {
        public string Status { get; set; } = default!;
        public List<GoogleGeocodeResult> Results { get; set; } = new();
    }
}
