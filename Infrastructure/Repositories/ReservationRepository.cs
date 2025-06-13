using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ReservationRepository : GenericRepository<Reservation>, IReservationRepository
    {
        public ReservationRepository(BicycleRentalDbContext _dbContext) : base(_dbContext)
        {
        }
        public async Task<Reservation?> GetWithDetailsByIdAsync(Guid id)
        {
            var result = await _dbContext.Reservations
                 .Include(r => r.Guest)
                 .Include(r => r.Address)
                 .Include(r => r.Bicycles)
                 .Include(r => r.Payment)
                 .FirstOrDefaultAsync(r => r.Id == id);
            return result;
        }
    }
}
