using Microsoft.Data.Entity;
using ParkingATHWeb.DataAccess.Common;
using ParkingATHWeb.DataAccess.Interfaces;
using ParkingATHWeb.Model.Concrete;

namespace ParkingATHWeb.DataAccess.Repositories
{
    public class PriceTresholdRepository:GenericRepository<PriceTreshold>,IPriceTresholdRepository
    {
        private readonly DbSet<PriceTreshold> _dbSet;

         public PriceTresholdRepository(IDatabaseFactory factory)
            : base(factory)
        {
            _dbSet = factory.Get().Set<PriceTreshold>();
        }
    }
}
