using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.Address;

namespace Application.DTO.GeoLocation
{
    public class AddressWithDeliveryInfoDto : AddressDto
    {
        public bool IsWithinDeliveryRange { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public string? Message { get; set; }
    }
}
