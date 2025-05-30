using Application.Extensions;
using Application.Services;
using BicycleRentalApi.Middleware;
using Domain.Interfaces;
using Hangfire;
using Infrastructure.Extensions;
using Infrastructure.Seeder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplication();
builder.Services.AddInfrastructures(builder.Configuration);
builder.Services.AddScoped<ExceptionHandlingMiddleware>();

//Hangfire configuration
builder.Services.AddHangfire(config =>
    config.UseSqlServerStorage(builder.Configuration.GetConnectionString("BicycleRentalDb")));
builder.Services.AddHangfireServer();

// w pipeline:
//TODO
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
var app = builder.Build();
app.UseMiddleware<ExceptionHandlingMiddleware>();
//TODO
app.UseCors("AllowAll");

//Hangfire Monitor
app.UseHangfireDashboard("/hangfire");
RecurringJob.AddOrUpdate<IReservationCleanerService>(
    "cleanup-expired-reservations",
    service => service.CleanupExpiredReservationsAsync(),
    "*/5 * * * *");
//Seeder
var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<BicycleRentalApiSeeder>();
await seeder.Seed();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
