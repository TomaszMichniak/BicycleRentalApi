using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IReservationRepository : IGenericRepository<Reservation>
    {
        public Task<Reservation?> GetWithDetailsByIdAsync(Guid id);
    }
}
