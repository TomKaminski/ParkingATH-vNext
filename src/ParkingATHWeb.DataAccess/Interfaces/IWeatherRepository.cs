using System;
using System.Threading.Tasks;
using ParkingATHWeb.DataAccess.Common;
using ParkingATHWeb.Model.Concrete;

namespace ParkingATHWeb.DataAccess.Interfaces
{
    public interface IWeatherRepository : IGenericRepository<Weather, Guid>, IDependencyRepository
    {
        Task<Weather> GetMostRecentWeather();
    }
}
