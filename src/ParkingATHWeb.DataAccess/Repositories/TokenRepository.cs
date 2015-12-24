using Microsoft.Data.Entity;
using ParkingATHWeb.DataAccess.Common;
using ParkingATHWeb.DataAccess.Interfaces;
using ParkingATHWeb.Model.Concrete;

namespace ParkingATHWeb.DataAccess.Repositories
{
    public class TokenRepository : GenericRepository<Token, long>, ITokenRepository
    {
        private readonly DbSet<Token> _dbset;

        public TokenRepository(IDatabaseFactory factory)
            : base(factory)
        {
            _dbset = factory.Get().Set<Token>();
        }

    }
}
