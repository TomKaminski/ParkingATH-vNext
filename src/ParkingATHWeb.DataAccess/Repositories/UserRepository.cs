using Microsoft.Data.Entity;
using ParkingATHWeb.DataAccess.Common;
using ParkingATHWeb.DataAccess.Interfaces;
using ParkingATHWeb.Model.Concrete;

namespace ParkingATHWeb.DataAccess.Repositories
{
    public class UserRepository  : GenericRepository<User, int>, IUserRepository
    {
        private readonly DbSet<User> _dbSet;

        public UserRepository(IDatabaseFactory factory)
            : base(factory)
        {
            _dbSet = factory.Get().Set<User>();
        }
    }
}
