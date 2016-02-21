using System;
using System.Linq;
using AutoMapper;
using ParkingATHWeb.Business.HelperClasses;
using ParkingATHWeb.Contracts.DTO.Weather;
using ParkingATHWeb.Contracts.DTO.WeatherInfo;
using ParkingATHWeb.Model.Concrete;
using ParkingATHWeb.Shared.Helpers;
using Weather = ParkingATHWeb.Model.Concrete.Weather;

namespace ParkingATHWeb.Resolver.Mappings
{
    public partial class BackendMappingProvider
    {
        private static void InitializeWeatherMappings()
        {
            Mapper.CreateMap<WeatherDto, Weather>().IgnoreNotExistingProperties();

            Mapper.CreateMap<Weather, WeatherDto>()
                .ForMember(x => x.WeatherInfo, opt => opt.MapFrom(src => src.WeatherInfo.Select(Mapper.Map<WeatherInfoDto>)))
                .IgnoreNotExistingProperties();

            Mapper.CreateMap<WeatherInfo, WeatherInfoDto>()
               .IgnoreNotExistingProperties();

            Mapper.CreateMap<WeatherInfoDto, WeatherInfo>()
                .IgnoreNotExistingProperties();

            Mapper.CreateMap<Business.HelperClasses.Weather, WeatherInfo>()
                .ForMember(x => x.WeatherDescription, src => src.MapFrom(a => a.description))
                .ForMember(x => x.WeatherConditionId, src => src.MapFrom(a => a.id))
                .ForMember(x => x.WeatherMain, src => src.MapFrom(a => a.main))
                .ForMember(x => x.Id, opt => opt.Ignore())
                .IgnoreNotExistingProperties();

            Mapper.CreateMap<WeatherHelper, WeatherDto>()
                .ForMember(x => x.DateOfRead, opt => opt.UseValue(DateTime.Now))
                .ForMember(x => x.ValidToDate, opt => opt.UseValue(DateTime.Now.AddMinutes(30)))
                .ForMember(x => x.Humidity, opt => opt.MapFrom(m => m.main.humidity))
                .ForMember(x => x.Pressure, opt => opt.MapFrom(m => m.main.pressure))
                .ForMember(x => x.Temperature, opt => opt.MapFrom(m => Math.Round(m.main.temp - 273.15, 0)))
                .ForMember(x => x.WeatherInfo, opt => opt.MapFrom(m => m.weather.Select(Mapper.Map<WeatherInfo>).ToList()))
                .ForMember(x => x.Clouds, opt => opt.MapFrom(src => src.clouds.all))
                .ForMember(x => x.Id, opt => opt.Ignore());
        }
    }
}
