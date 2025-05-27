using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IBicycleRepository: IGenericRepository<Bicycle>
    {
        public Task<List<Bicycle>> GetAvailableBicyclesByDates(DateTime start, DateTime end);
        public Task<List<Bicycle>> GetAvailableBicycles(DateTime startDate, DateTime endDate, BicycleSize size, string name);
    }
}
