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
                    ImageUrl="//"

                },
                 new Bicycle()
                {
                    Name="Rockrider E-ST 500",
                    Description="Przeznaczony na wycieczki terenowe, z przewyższeniami (pagórkowate trasy i średnie góry).",
                    PricePerDay=150,
                    Size=BicycleSize.M,
                    ImageUrl="//"

                },new Bicycle()
                {
                    Name="Rockrider E-ST 500",
                    Description="Przeznaczony na wycieczki terenowe, z przewyższeniami (pagórkowate trasy i średnie góry).",
                    PricePerDay=150,
                    Size=BicycleSize.S,
                    ImageUrl="//"

                },
                 new Bicycle()
                {
                    Name="Rockrider E-ST 500",
                    Description="Przeznaczony na wycieczki terenowe, z przewyższeniami (pagórkowate trasy i średnie góry).",
                    PricePerDay=150,
                    Size=BicycleSize.L,
                    ImageUrl="//"

                },new Bicycle()
                {
                    Name="Rockrider E-ST 500",
                    Description="Przeznaczony na wycieczki terenowe, z przewyższeniami (pagórkowate trasy i średnie góry).",
                    PricePerDay=150,
                    Size=BicycleSize.L,
                    ImageUrl="//"

                },
                 new Bicycle()
                {
                    Name="Rockrider E-ST 500",
                    Description="Przeznaczony na wycieczki terenowe, z przewyższeniami (pagórkowate trasy i średnie góry).",
                    PricePerDay=150,
                    Size=BicycleSize.XL,
                    ImageUrl="//"

                },

            };
            return bicycles;
        }
    }
}
