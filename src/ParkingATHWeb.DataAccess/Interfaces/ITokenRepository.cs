using ParkingATHWeb.DataAccess.Common;
using ParkingATHWeb.Model.Concrete;

namespace ParkingATHWeb.DataAccess.Interfaces
{
    public interface ITokenRepository:IGenericRepository<Token, long>, IDependencyRepository
    {
    }
}
