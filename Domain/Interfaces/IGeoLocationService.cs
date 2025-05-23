using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IGeoLocationService
    {
        Task<(Address address,double Lat, double Lng)?> GetCoordinatesAsync(Address address);
        Task<(Address address, double Lat, double Lng)?> ReverseGeocodeAsync(double lat, double lng);
        bool IsWithinDeliveryRange(double lat, double lng);
    }
}
