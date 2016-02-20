using System;
using System.Threading.Tasks;
using ParkingATHWeb.Contracts.Common;
using ParkingATHWeb.Contracts.DTO.Weather;
using ParkingATHWeb.Contracts.Services.Base;

namespace ParkingATHWeb.Contracts.Services
{
    public interface IWeatherService : IEntityService<WeatherDto,Guid>, IDependencyService
    {
        Task<ServiceResult<WeatherDto>> GetLatestWeatherDataAsync();
    }
}
