using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Infrastructure.Database;

namespace Infrastructure.Seeder
{
    public class BicycleRentalApiSeeder
    {
        private readonly BicycleRentalDbContext _dbContext;
      //  private readonly IPasswordHasher<User> _passwordHasher;

        public BicycleRentalApiSeeder(BicycleRentalDbContext dbContext
            //,IPasswordHasher<User> passwordHasher
            )
        {
            _dbContext = dbContext;
            //_passwordHasher = passwordHasher;
        }
        public async Task Seed()
        {

            if (!_dbContext.Bicycles.Any())
            {
                var bicycles = GetBicycles();
                _dbContext.Bicycles.AddRange(bicycles);
                await _dbContext.SaveChangesAsync();
            }
            if(!_dbContext.Addresses.Any())
            {
                var addresses = GetAddresses();
                _dbContext.Addresses.AddRange(addresses);
                await _dbContext.SaveChangesAsync();
            }
        }
        private IEnumerable<Address> GetAddresses()
        {
            var addresses = new List<Address>() {
                new Address()
                {
                    Street="ul. Nędzy Kubińca 255",
                    City="Kościelisko",
                    PostalCode="34-511",
                    Type=AddressType.PickupPoint
                },  new Address()
                {
                    Street="113",
                    City="Nowe Bystre",
                    PostalCode="34-521",
                    Type=AddressType.PickupPoint
                },
            };
            return addresses;
        }
        private IEnumerable<Bicycle> GetBicycles()
        {
            var bicycles = new List<Bicycle>()
            {
                new Bicycle()
                {
                    Name="Rockrider E-ST 500",
                    Description="Przeznaczony na wycieczki terenowe, z przewyższeniami (pagórkowate trasy i średnie góry).",
                    PricePerDay=150,
                    Size=BicycleSize.M,
                    ImageUrl="./src/assets/rower-elektryczny-gorski-mtb-rockrider-e-st-500-275.jpg"

                },
                 new Bicycle()
                {
                    Name="Rockrider E-ST 500",
                    Description="Przeznaczony na wycieczki terenowe, z przewyższeniami (pagórkowate trasy i średnie góry).",
                    PricePerDay=150,
                    Size=BicycleSize.M,
                    ImageUrl="./src/assets/rower-elektryczny-gorski-mtb-rockrider-e-st-500-275.jpg"

                },new Bicycle()
                {
                    Name="Rockrider E-ST 500",
                    Description="Przeznaczony na wycieczki terenowe, z przewyższeniami (pagórkowate trasy i średnie góry).",
                    PricePerDay=150,
                    Size=BicycleSize.S,
                    ImageUrl="./src/assets/rower-elektryczny-gorski-mtb-rockrider-e-st-500-275.jpg"

                },
                 new Bicycle()
                {
                    Name="Rockrider E-ST 500",
                    Description="Przeznaczony na wycieczki terenowe, z przewyższeniami (pagórkowate trasy i średnie góry).",
                    PricePerDay=150,
                    Size=BicycleSize.L,
                    ImageUrl="./src/assets/rower-elektryczny-gorski-mtb-rockrider-e-st-500-275.jpg"

                },new Bicycle()
                {
                    Name="Rockrider E-ST 500",
                    Description="Przeznaczony na wycieczki terenowe, z przewyższeniami (pagórkowate trasy i średnie góry).",
                    PricePerDay=150,
                    Size=BicycleSize.L,
                    ImageUrl="./src/assets/rower-elektryczny-gorski-mtb-rockrider-e-st-500-275.jpg"

                },
                 new Bicycle()
                {
                    Name="Rockrider E-ST 500",
                    Description="Przeznaczony na wycieczki terenowe, z przewyższeniami (pagórkowate trasy i średnie góry).",
                    PricePerDay=150,
                    Size=BicycleSize.XL,
                    ImageUrl="./src/assets/rower-elektryczny-gorski-mtb-rockrider-e-st-500-275.jpg"
                        
                },

            };
            return bicycles;
        }
    }
}
