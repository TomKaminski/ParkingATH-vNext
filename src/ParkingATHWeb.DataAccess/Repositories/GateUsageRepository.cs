using Microsoft.Data.Entity;
using ParkingATHWeb.DataAccess.Common;
using ParkingATHWeb.DataAccess.Interfaces;
using ParkingATHWeb.Model.Concrete;

namespace ParkingATHWeb.DataAccess.Repositories
{
    public class GateUsageRepository : GenericRepository<GateUsage>, IGateUsageRepository
    {
        private readonly DbSet<GateUsage> _dbset;

        public GateUsageRepository(IDatabaseFactory factory)
            : base(factory)
        {
            _dbset = factory.Get().Set<GateUsage>();
        }
    }
}
