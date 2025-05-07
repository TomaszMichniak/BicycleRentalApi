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
    public class BicycleRepository : GenericRepository<Bicycle>, IBicycleRepository
    {
        public BicycleRepository(BicycleRentalDbContext dbContext) : base(dbContext)
        {
        }
    }
}
