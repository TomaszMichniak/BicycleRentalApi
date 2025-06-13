using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string body);
        public Task<string> GetReservationConfirmationHTML(Reservation reservation);
    }
}
