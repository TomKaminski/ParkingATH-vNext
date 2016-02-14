using Microsoft.Data.Entity;
using ParkingATHWeb.DataAccess.Common;
using ParkingATHWeb.DataAccess.Interfaces;
using ParkingATHWeb.Model.Concrete;

namespace ParkingATHWeb.DataAccess.Repositories
{
    public class UserPreferencesRepository  : GenericRepository<UserPreferences, int>, IUserPreferencesRepository
    {
        private readonly DbSet<UserPreferences> _dbSet;

        public UserPreferencesRepository(IDatabaseFactory factory)
            : base(factory)
        {
            _dbSet = factory.Get().Set<UserPreferences>();
        }
    }
}
