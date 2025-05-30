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
    public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(BicycleRentalDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<Payment?> GetPaymentByOrderId(string Id)
        {
            return await _dbContext.Payments.Where(p => p.PayuOrderId == Id).FirstOrDefaultAsync();
        }
    }
}
