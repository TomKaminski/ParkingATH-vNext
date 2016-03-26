using System.Linq;
using System.Threading.Tasks;
using ParkingATHWeb.DataAccess.Common;
using ParkingATHWeb.Model.Concrete;

namespace ParkingATHWeb.DataAccess.Interfaces
{
    public interface IUserRepository : IGenericRepository<User, int>, IDependencyRepository
    {
    }
}
