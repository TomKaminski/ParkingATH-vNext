using ParkingATHWeb.DataAccess.Common;
using ParkingATHWeb.Model.Concrete;

namespace ParkingATHWeb.DataAccess.Interfaces
{
    public interface IOrderRepository:IGenericRepository<Order>, IDependencyRepository
    {
    }
}
