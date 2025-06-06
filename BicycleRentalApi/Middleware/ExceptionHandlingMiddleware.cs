using Application.Exceptions;
using System.Net;
using System.Text.Json;

namespace BicycleRentalApi.Middleware
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (InvalidDataException ex)
            {
                await HandleExceptionAsync(context, HttpStatusCode.BadRequest, "Invalid data", ex.Message);
            }
            catch (AddressResolutionException ex)
            {
                await HandleExceptionAsync(context, HttpStatusCode.BadRequest, "AddressResolutionFailed", ex.Message);
            }

            catch (DeliveryRangeExceededException ex)
            {
                await HandleExceptionAsync(context, HttpStatusCode.BadRequest, "AddressOutOfDeliveryRange", ex.Message);
            }
            catch (BicyclesUnavailableException ex)
            {
                await HandleExceptionAsync(context, HttpStatusCode.BadRequest, "BicyclesUnavailable", ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception");
                await HandleExceptionAsync(context, HttpStatusCode.InternalServerError, "ServerError", "Something went wrong");
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, string error, string message)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var response = new
            {
                error,
                message
            };

            var json = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(json);
        }
    }
}
