using Domain.Entities;
using Infrastructure.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Seeder
{
    public class BicycleRentalApiSeeder
    {
        private readonly BicycleRentalDbContext _dbContext;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IConfiguration _configuration;

        public BicycleRentalApiSeeder(BicycleRentalDbContext dbContext,
            IPasswordHasher<User> passwordHasher, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
            _configuration = configuration;
        }

        public async Task Seed()
        {

            if (!_dbContext.Bicycles.Any())
            {
                var bicycles = GetBicycles();
                _dbContext.Bicycles.AddRange(bicycles);
                await _dbContext.SaveChangesAsync();
            }
            if (!_dbContext.Addresses.Any())
            {
                var addresses = GetAddresses();
                _dbContext.Addresses.AddRange(addresses);
                await _dbContext.SaveChangesAsync();
            }
            if (!_dbContext.Roles.Any())
            {
                var roles = GetRoles();
                _dbContext.Roles.AddRange(roles);
                await _dbContext.SaveChangesAsync();
            }
            if (!_dbContext.Users.Any())
            {
                var users = await GetUsers();
                _dbContext.Users.AddRange(users);
                await _dbContext.SaveChangesAsync();
            }
        }
        private async Task<IEnumerable<User>> GetUsers()
        {
            var users = new List<User>();
            var roleAdmin = await _dbContext.Roles.FirstOrDefaultAsync(r => r.Name == "Admin");
            var admin = new User()
            {
                Email = _configuration["Users:AdminEmail"],
                FirstName = "Admin",
                LastName = "Admin",
                Phone = "",
                Role = roleAdmin,

            };
            admin.PasswordHash = _passwordHasher.HashPassword(admin, _configuration["Users:AdminPassword"]);
            users.Add(admin);
            var roleManager = await _dbContext.Roles.FirstOrDefaultAsync(r => r.Name == "Manager");
            var manager = new User()
            {
                Email = _configuration["Users:ManagerEmail"],
                FirstName = "Manager",
                LastName = "Manager",
                Phone = "",
                Role = roleManager,
            };
            manager.PasswordHash = _passwordHasher.HashPassword(manager, _configuration["Users:ManagerPassword"]);
            users.Add(manager);
            return users;
        }
        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    Name="Admin",
                },  new Role()
                {
                    Name="Manager",
                }
            };
            return roles;
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
