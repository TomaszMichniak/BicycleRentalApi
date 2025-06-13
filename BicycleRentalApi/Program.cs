using System.Globalization;
using Application.Extensions;
using BicycleRentalApi.Extenstions;
using BicycleRentalApi.Middleware;
using Domain.Interfaces;
using Hangfire;
using Infrastructure.Extensions;
using Infrastructure.Seeder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new UtcDateTimeConverter());
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.AddPresentation();
builder.Services.AddApplication();
builder.Services.AddInfrastructures(builder.Configuration);


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
//Culutre
var supportedCultures = new[] { new CultureInfo("pl-PL") };
var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("pl-PL"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
};

app.UseRequestLocalization(localizationOptions);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.MapControllers();

app.Run();
