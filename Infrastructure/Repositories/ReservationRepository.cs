using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Database;

namespace Infrastructure.Repositories
{
    public class ReservationRepository: GenericRepository<Reservation>, IReservationRepository
    {
        public ReservationRepository(BicycleRentalDbContext _dbContext) : base(_dbContext)
        {
        }

        public Task<Reservation> CreateReservationAsync(Reservation reservation )
        {
            throw new NotImplementedException();
        }
    }
}
