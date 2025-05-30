using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Services;
using Domain.Interfaces;
using Domain.Specification;
using Infrastructure.Database;
using Infrastructure.Repositories;
using Infrastructure.Seeder;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
    public static class ServiceCollectionsExtensions
    {
        public static void AddInfrastructures(this IServiceCollection Services, IConfiguration configuration)
        {
            Services.AddHttpClient<IGeoLocationService, GeoLocationService>();
            Services.AddHttpClient<IPaymentGatewayService, PayUPaymentGatewayService>();
            var connectionString = configuration.GetConnectionString("BicycleRentalDb");
            Services.AddDbContext<IBicycleRentalDbContext, BicycleRentalDbContext>(options =>
                options.UseSqlServer(connectionString)
                    .EnableSensitiveDataLogging());
            Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            Services.AddScoped<IBicycleRepository, BicycleRepository>();
            Services.AddScoped<IReservationRepository, ReservationRepository>();
            Services.AddScoped<IPaymentRepository, PaymentRepository>();
            Services.AddScoped<IAddressRepository, AddressRepository>();
            Services.AddScoped<IGuestRepository, GuestRepository>();
            Services.AddScoped<IReservationCleanerService, ReservationCleanerService>();
            Services.AddScoped<BicycleRentalApiSeeder>();
            Services.AddScoped(typeof(ISpecification<>), typeof(Specification<>));
        }
    }
}
