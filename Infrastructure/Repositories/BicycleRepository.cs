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
                   br.EndDate >= start && (
                   br.Payment.Status == PaymentStatus.Pending ||
                   br.Payment.Status == PaymentStatus.Paid ||
                   br.Payment.Status == PaymentStatus.WaitingForConfirmation
               )))
               .ToListAsync();

            return availableBicycles;
        }
        public async Task<List<Bicycle>> GetAvailableBicycles(DateTime startDate, DateTime endDate, BicycleSize size, string name)
        {
            return await _dbContext.Bicycles
                .Where(b => b.Size == size && b.Name == name)
                .Where(b => !b.Reservations.Any(r =>
                    r.StartDate < endDate &&
                    r.EndDate > startDate && (
                    r.Payment.Status == PaymentStatus.Pending ||
                    r.Payment.Status == PaymentStatus.Paid ||
                    r.Payment.Status == PaymentStatus.WaitingForConfirmation
                )))
                .ToListAsync();
        }
    }
}
