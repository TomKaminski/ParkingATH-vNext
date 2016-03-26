using System.Linq;
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

        //public IQueryable<User> GetUsersForAdmin()
        //{
        //    var queryWithIncludes =
        //        _dbSet.Include(x => x.UserPreferences).Include(x => x.Orders).Include(x => x.GateUsages).Select(x=>new User
        //        {
        //            Orders = x.Orders.OrderByDescending(a=>a.Date).Take(3),

        //        });


        //}
    }
}
