using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.GeoLocation
{
    public class Location
    {
        public double Lat { get; set; }
        public double Lng { get; set; }
        public bool? IsWithinDeliveryRange { get; set; }
        public string? Message { get; set; }
    }
}
