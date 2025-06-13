using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Application.CQRS.Payment.Command.Create;
using Application.DTO.Payment;
using Domain.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services
{
    public class PayUPaymentGatewayService : IPaymentGatewayService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public PayUPaymentGatewayService(HttpClient httpClient, IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }
        public async Task<OrderResponse> CreateOrderAsync(CreatePaymentCommand request)
        {
            var token = await GetAccessToken();

            var client = new HttpClient(new HttpClientHandler { AllowAutoRedirect = false });
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var posId = _configuration["PayU:PosId"];
            var notifyUrl = _configuration["PayU:NotifyUrl"];
            var continueUrl = _configuration["PayU:ContinueUrl"];
            var orderUrl = $"{_configuration["PayU:ApiBaseUrl"]}{_configuration["PayU:OrderEndpoint"]}";

            var orderRequest = new
            {
                notifyUrl = notifyUrl,
                customerIp = request.CustomerIp,
                merchantPosId = posId,
                description = request.Description,
                currencyCode = "PLN",
                totalAmount = (request.TotalAmount * 100).ToString("F0"),
                buyer = request.Buyer,
                products = request.Products,
                continueUrl = continueUrl + request.ReservationId
            };

            var content = new StringContent(JsonSerializer.Serialize(orderRequest), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(orderUrl, content);
            if (response.StatusCode != HttpStatusCode.Found && !response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Unexpected status code: {response.StatusCode}");
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var orderResponse = JsonSerializer.Deserialize<OrderResponse>(jsonResponse);
            return orderResponse ?? throw new InvalidOperationException("Empty PayU response");
        }

        public async Task CancelTransactionAsync(string payuOrderId)
        {
            var token = await GetAccessToken();
            var baseUrl = _configuration["PayU:ApiBaseUrl"];
            var orderUrl = _configuration["PayU:OrderEndpoint"];
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var url = $"{baseUrl}{orderUrl}/{payuOrderId}";
            var response = await _httpClient.DeleteAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                throw new Exception($"Failed to cancel transaction in PayU: {content}");
            }
        }

        public async Task RefundTransactionAsync(string payuOrderId, string description = "Zwrot płatności")
        {
            var token = await GetAccessToken();
            var baseUrl = _configuration["PayU:ApiBaseUrl"];
            var orderUrl = _configuration["PayU:OrderEndpoint"];
            var url = $"{baseUrl}{orderUrl}/{payuOrderId}/refunds";

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var refundRequest = new
            {
                refund = new
                {
                    description = description,
                }
            };

            var content = new StringContent(JsonSerializer.Serialize(refundRequest), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Refund failed: {error}");
            }
        }
        private async Task<string> GetAccessToken()
        {
            var client = _httpClientFactory.CreateClient();

            var parameters = new Dictionary<string, string>
        {
            { "grant_type", "client_credentials" },
            { "client_id", _configuration["PayU:ClientId"] },
            { "client_secret", _configuration["PayU:ClientSecret"] }
        };
            var baseUrl = _configuration["PayU:ApiBaseUrl"];
            var oAuthEndpoint = _configuration["PayU:OAuthEndpoint"];

            var authUrl = $"{baseUrl}{oAuthEndpoint}";
            var request = new HttpRequestMessage(HttpMethod.Post, authUrl)
            {
                Content = new FormUrlEncodedContent(parameters)
            };
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var tokenData = JsonSerializer.Deserialize<TokenResponse>(json);
            return tokenData.AccessToken;
        }


    }
}
