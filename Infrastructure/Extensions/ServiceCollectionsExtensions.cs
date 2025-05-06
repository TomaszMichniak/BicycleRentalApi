using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Database;
using Infrastructure.Seeder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
    public static class ServiceCollectionsExtensions
    {
        public static void AddInfrastructures(this IServiceCollection Services, IConfiguration configuration)
        {
            Services.AddDbContext<BicycleRentalContext>();
          //  services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
          //  services.AddScoped<IApartmentRepository, ApartmentRepository>();
            Services.AddScoped<BicycleRentalApiSeeder>();
            //services.AddScoped(typeof(ISpecification<>), typeof(Specification<>));
        }
    }
}
