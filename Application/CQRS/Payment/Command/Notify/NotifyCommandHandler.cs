using Application.CQRS.Payment.Command.Notify;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text;

public class NotifyCommandHandler : IRequestHandler<NotifyCommand, bool>
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IConfiguration _configuration;
    private readonly IEmailService _emailService;

    public NotifyCommandHandler(IPaymentRepository paymentRepository, IConfiguration configuration, IEmailService emailService)
    {
        _paymentRepository = paymentRepository;
        _configuration = configuration;
        _emailService = emailService;
    }

    public async Task<bool> Handle(NotifyCommand request, CancellationToken cancellationToken)
    {
        if (!VerifySignature(request.SignatureHeader, request.RawBody))
            return false;

        JsonElement root;
        try
        {
            root = JsonSerializer.Deserialize<JsonElement>(request.RawBody);
        }
        catch
        {
            return false;
        }
        //TODO
        if (root.TryGetProperty("refund", out var element))
        {
            return true;
        }

        if (!root.TryGetProperty("order", out var orderElement))
            return false;

        var order = JsonSerializer.Deserialize<PayuOrder>(orderElement.GetRawText());
        if (order == null)
            return false;

        var payment = await _paymentRepository.GetPaymentByOrderId(order.OrderId);
        if (payment == null)
            return false;
        switch (order.Status.ToUpperInvariant())
        {
            case "PENDING":
                payment.Status = PaymentStatus.Pending;
                break;
            case "WAITING_FOR_CONFIRMATION":
                payment.Status = PaymentStatus.WaitingForConfirmation;
                break;
            case "COMPLETED":
                payment.Status = PaymentStatus.Paid;
                payment.PaidAt ??= DateTime.UtcNow;

                var subject = "Twoja rezerwacja została opłacona";
                var body = $"Dziękujemy za dokonanie płatności za rezerwację nr {payment.ReservationId}.";
                await _emailService.SendEmailAsync(order.Buyer.Email, subject, body);
                break;
            case "FAILED":
                payment.Status = PaymentStatus.Failed;
                break;
            case "CANCELED":
                payment.Status = PaymentStatus.Cancelled;
                break;
            default:
                return false;
        }

        await _paymentRepository.UpdateAsync(payment);
        return true;
    }
    private string ExtractSignature(string signatureHeader)
    {
        var parts = signatureHeader.Split(';');
        foreach (var part in parts)
        {
            var keyValue = part.Split('=');
            if (keyValue.Length == 2 && keyValue[0].Trim() == "signature")
            {
                return keyValue[1].Trim();
            }
        }
        return string.Empty;
    }
    private bool VerifySignature(string signatureHeader, string rawBody)
    {
        var secondKey = _configuration["PayU:SecondKey"];


        var signature = ExtractSignature(signatureHeader);

        var concatenated =rawBody + secondKey;

        using var md5 = MD5.Create();
        var hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(concatenated));
        var computedSignature = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        if (computedSignature == signature.ToLower())
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
