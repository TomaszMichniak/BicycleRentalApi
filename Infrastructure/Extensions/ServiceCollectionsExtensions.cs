using System.Text;
using Application.Services;
using Domain.Interfaces;
using Domain.Specification;
using Infrastructure.Database;
using Infrastructure.Repositories;
using Infrastructure.Seeder;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

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
            Services.AddScoped<IUserRepository, UserRepository>();
            Services.AddScoped<IGuestRepository, GuestRepository>();
            Services.AddScoped<IReservationCleanerService, ReservationCleanerService>();
            Services.AddScoped<BicycleRentalApiSeeder>();
            Services.AddScoped<IEmailService, EmailService>();
            Services.AddScoped(typeof(ISpecification<>), typeof(Specification<>));
            Services.AddScoped<IJwtService, JwtService>();

            //JWT Settings

            var jwtSettings = new JwtSettings();
            configuration.GetSection("JwtSettings").Bind(jwtSettings);
            Services.AddSingleton(jwtSettings);
            Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings.Key))
                };
            });
            Services.AddAuthorization();
            Services.AddAuthentication();
        }
    }
}
