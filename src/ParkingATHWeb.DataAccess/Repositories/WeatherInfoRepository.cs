using System;
using Microsoft.Data.Entity;
using ParkingATHWeb.DataAccess.Common;
using ParkingATHWeb.DataAccess.Interfaces;
using ParkingATHWeb.Model.Concrete;

namespace ParkingATHWeb.DataAccess.Repositories
{
    public class WeatherInfoRepository  : GenericRepository<WeatherInfo, Guid>, IWeatherInfoRepository
    {
        private readonly DbSet<WeatherInfo> _dbSet;

        public WeatherInfoRepository(IDatabaseFactory factory)
            : base(factory)
        {
            _dbSet = factory.Get().Set<WeatherInfo>();
        }
    }
}
