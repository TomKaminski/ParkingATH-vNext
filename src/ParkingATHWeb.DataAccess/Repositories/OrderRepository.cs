using Microsoft.Data.Entity;
using ParkingATHWeb.DataAccess.Common;
using ParkingATHWeb.DataAccess.Interfaces;
using ParkingATHWeb.Model.Concrete;

namespace ParkingATHWeb.DataAccess.Repositories
{
    public class OrderRepository:GenericRepository<Order, long>,IOrderRepository
    {
        private readonly DbSet<Order> _dbset;

        public OrderRepository(IDatabaseFactory factory)
            : base(factory)
        {
            _dbset = factory.Get().Set<Order>();
        }
    }
}
