using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class BicycleRepository : GenericRepository<Bicycle>, IBicycleRepository
    {
        public BicycleRepository(BicycleRentalDbContext _dbContext) : base(_dbContext)
        {
        }

        public async Task<List<Bicycle>> GetAvailableBicyclesByDates(DateTime start, DateTime end)
        {
             var availableBicycles = await _dbContext.Bicycles
                .Where(b => !b.Reservations.Any(br =>
                    br.StartDate <= end &&
                    br.EndDate >= start))
                .ToListAsync();

            return availableBicycles;
        }
    }
}
