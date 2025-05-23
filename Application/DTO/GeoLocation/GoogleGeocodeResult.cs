using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.GeoLocation
{
    public class GoogleGeocodeResult
    {
        public Geometry Geometry { get; set; } = default!;
        public List<AddressComponent> AddressComponents { get; set; } = new();
    }
}
