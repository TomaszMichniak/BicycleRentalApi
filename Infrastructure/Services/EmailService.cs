using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

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
            var port=_configuration["GmailOptions:Port"];
            var host=_configuration["GmailOptions:Host"];
            
            if ( fromEmail==null || password == null || port == null || host == null)
            {
                throw new InvalidOperationException();
            }
            MailMessage mailMessage = new MailMessage()
            {
                From=new MailAddress(fromEmail),
                Subject=subject,
                Body=body,
            };
            mailMessage.To.Add(to);

            using var smtpClient = new SmtpClient();
            smtpClient.Host = host;
            smtpClient.Port = int.Parse(port);
            smtpClient.Credentials=new NetworkCredential(fromEmail, password);
            smtpClient.EnableSsl = true;

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
