using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using ParkingATHWeb.DataAccess.Common;
using ParkingATHWeb.DataAccess.Interfaces;
using ParkingATHWeb.Model.Concrete;

namespace ParkingATHWeb.DataAccess.Repositories
{
    public class WeatherRepository : GenericRepository<Weather, Guid>, IWeatherRepository
    {
        private readonly DbSet<Weather> _dbSet;

        public WeatherRepository(IDatabaseFactory factory)
            : base(factory)
        {
            _dbSet = factory.Get().Set<Weather>();
        }

        public async Task<Weather> GetMostRecentWeather()
        {
            return (await _dbSet.OrderByDescending(x => x.DateOfRead).Include(x => x.WeatherInfo).ToListAsync()).FirstOrDefault();
        }
    }
}
