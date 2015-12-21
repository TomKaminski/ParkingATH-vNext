using ParkingATHWeb.DataAccess.Common;
using ParkingATHWeb.Model.Concrete;

namespace ParkingATHWeb.DataAccess.Interfaces
{
    public interface IPriceTresholdRepository:IGenericRepository<PriceTreshold>, IDependencyRepository
    {
    }
}
