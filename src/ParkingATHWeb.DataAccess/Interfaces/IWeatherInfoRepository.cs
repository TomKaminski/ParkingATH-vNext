using System;
using ParkingATHWeb.DataAccess.Common;
using ParkingATHWeb.Model.Concrete;

namespace ParkingATHWeb.DataAccess.Interfaces
{
    public interface IWeatherInfoRepository : IGenericRepository<WeatherInfo, Guid>, IDependencyRepository
    {
    }
}
