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
    public class AddressRepository: GenericRepository<Address>, IAddressRepository
    {
        public AddressRepository(BicycleRentalContext dbContext) : base(dbContext)
        {
        }
    }
}
