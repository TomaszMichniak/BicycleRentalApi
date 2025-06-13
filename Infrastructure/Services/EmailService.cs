using System.Net;
using System.Net.Mail;
using System.Text;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var fromEmail = _configuration["GmailOptions:Email"];
            var password = _configuration["GmailOptions:Password"];
            var port = _configuration["GmailOptions:Port"];
            var host = _configuration["GmailOptions:Host"];

            if (fromEmail == null || password == null || port == null || host == null)
            {
                throw new InvalidOperationException();
            }
            MailMessage mailMessage = new MailMessage()
            {
                From = new MailAddress(fromEmail),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(to);

            using var smtpClient = new SmtpClient();
            smtpClient.Host = host;
            smtpClient.Port = int.Parse(port);
            smtpClient.Credentials = new NetworkCredential(fromEmail, password);
            smtpClient.EnableSsl = true;

            await smtpClient.SendMailAsync(mailMessage);
        }
        public async Task<string> GetReservationConfirmationHTML(Reservation reservation)
        {
            var fromEmail = _configuration["GmailOptions:Email"];
            var domain = _configuration["App:Domain"];
            var continueUrl = $"{_configuration["PayU:ContinueUrl"]}{reservation.Id}";
            var templatePath = Path.Combine(AppContext.BaseDirectory, "Templates", "reservation-confirmation.html");
            string html = await File.ReadAllTextAsync(templatePath);
            var addressString = $"{(reservation.Address.Type == AddressType.PickupPoint ? "Odbiór osobisty:" : "Dostawa:")} {reservation.Address.City} {reservation.Address.Street} {reservation.Address.PostalCode}";

            var groupedBikes = reservation.Bicycles
                .GroupBy(b => new { b.Name, b.Size })
                .Select(g => new
                {
                    Name = g.Key.Name,
                    Size = g.Key.Size,
                    Count = g.Count()
                });
            var bikesHtml = new StringBuilder();
            foreach (var group in groupedBikes)
            {
                bikesHtml.AppendLine($"<li>{group.Name} - Rozmiar: {group.Size}, Ilość: {group.Count}</li>");
            }
            html = html
            .Replace("{{FirstName}}", reservation.Guest.FirstName)
            .Replace("{{ReservationId}}", reservation.Id.ToString().Substring(0, 8).ToUpper())
            .Replace("{{StartDate}}", reservation.StartDate.ToLocalTime().ToShortDateString())
            .Replace("{{EndDate}}", reservation.EndDate.ToShortDateString())
            .Replace("{{Address}}", addressString)
            .Replace("{{DeliveryHours}}", reservation.DeliveryHours)
            .Replace("{{GroupedBicycles}}", bikesHtml.ToString())
            .Replace("{{TotalCount}}", reservation.TotalPrice.ToString())
            .Replace("{{ReservationLink}}", continueUrl)
            .Replace("{{FromEmail}}", fromEmail)
            .Replace("{{Domain}}", domain);

            return html;
        }
    }
}
