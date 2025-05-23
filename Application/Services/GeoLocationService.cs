using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Application.DTO.Address;
using Application.DTO.GeoLocation;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Application.Services
{
    public class GeoLocationService : IGeoLocationService
    {

        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private const double WarehouseLat = 49.3325;
        private const double WarehouseLng = 19.9247;
        private const double MaxDeliveryDistanceKm = 25;
        public GeoLocationService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["GoogleApi:ApiKey"];
            if (string.IsNullOrEmpty(_apiKey))
                throw new ArgumentException("Google Maps API key is not configured.");
        }

        public async Task<(Address address, double Lat, double Lng)?> GetCoordinatesAsync(Address address)
        {
            if (address == null) throw new ArgumentNullException(nameof(address));

            var fullAddress = $"{address.Street}, {address.City}, {address.PostalCode}";
            var url = $"https://maps.googleapis.com/maps/api/geocode/json?address={Uri.EscapeDataString(fullAddress)}&key={_apiKey}";

            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;

            var firstResult = root.GetProperty("results").EnumerateArray().FirstOrDefault();
            if (firstResult.ValueKind == JsonValueKind.Undefined)
                return null;

            var location = firstResult.GetProperty("geometry").GetProperty("location");
            var lat = location.GetProperty("lat").GetDouble();
            var lng = location.GetProperty("lng").GetDouble();

            // Opcjonalnie: możesz ustandaryzować adres na podstawie danych z Google
            var updatedAddress = new Address
            {
                Street = $"{GetComponent(firstResult, "route")} {GetComponent(firstResult, "street_number")}".Trim(),
                City = GetComponent(firstResult, "locality") != "" ? GetComponent(firstResult, "locality") : GetComponent(firstResult, "postal_town"),
                PostalCode = GetComponent(firstResult, "postal_code")
            };

            return (updatedAddress, lat, lng);
        }

        public async Task<(Address address, double Lat, double Lng)?> ReverseGeocodeAsync(double lat, double lng)
        {
            var latStr = lat.ToString(CultureInfo.InvariantCulture);
            var lngStr = lng.ToString(CultureInfo.InvariantCulture);
            var url = $"https://maps.googleapis.com/maps/api/geocode/json?latlng={latStr},{lngStr}&key={_apiKey}";

            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;

            var firstResult = root.GetProperty("results").EnumerateArray().FirstOrDefault();
            if (firstResult.ValueKind == JsonValueKind.Undefined)
                return null;

            string GetComponent(string type)
            {
                foreach (var c in firstResult.GetProperty("address_components").EnumerateArray())
                {
                    foreach (var t in c.GetProperty("types").EnumerateArray())
                    {
                        if (t.GetString() == type)
                            return c.GetProperty("long_name").GetString() ?? "";
                    }
                }
                return "";
            }

            var address = new Address
            {
                Street = $"{GetComponent("route")} {GetComponent("street_number")}".Trim(),
                City = GetComponent("locality") != "" ? GetComponent("locality") : GetComponent("postal_town"),
                PostalCode = GetComponent("postal_code")
            };

            return (address, lat, lng);
        }
        public bool IsWithinDeliveryRange(double lat, double lng)
        {
            var distance = GetDistanceFromLatLonInKm(WarehouseLat, WarehouseLng, lat, lng);
            return distance <= MaxDeliveryDistanceKm;
        }
        public static double GetDistanceFromLatLonInKm(double lat1, double lon1, double lat2, double lon2)
        {
            double R = 6371; 
            double dLat = ToRadians(lat2 - lat1);
            double dLon = ToRadians(lon2 - lon1);
            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return R * c;
        }

        private static double ToRadians(double deg)
        {
            return deg * (Math.PI / 180);
        }
        private static string GetComponent(JsonElement result, string type)
        {
            foreach (var c in result.GetProperty("address_components").EnumerateArray())
            {
                foreach (var t in c.GetProperty("types").EnumerateArray())
                {
                    if (t.GetString() == type)
                        return c.GetProperty("long_name").GetString() ?? "";
                }
            }
            return "";
        }
    }
}
