using AutoMapper;
using ParkingATHWeb.Areas.Admin.ViewModels.User;
using ParkingATHWeb.Areas.Portal.ViewModels.User;
using ParkingATHWeb.Areas.Portal.ViewModels.Weather;
using ParkingATHWeb.Contracts.DTO;
using ParkingATHWeb.Contracts.DTO.User;
using ParkingATHWeb.Contracts.DTO.Weather;
using ParkingATHWeb.Contracts.DTO.WeatherInfo;
using ParkingATHWeb.Models;
using ParkingATHWeb.Shared.Helpers;

namespace ParkingATHWeb.Mappings
{
    public static partial class FrontendMappingsProvider
    {
        public static void InitMappings()
        {
            InitAccountMappings();

            Mapper.CreateMap<ParkingAthMessage, MessageDto>().IgnoreNotExistingProperties();
            Mapper.CreateMap<MessageDto, ParkingAthMessage>().IgnoreNotExistingProperties();

            Mapper.CreateMap<UserBaseDto, UserBaseViewModel>().IgnoreNotExistingProperties();
            Mapper.CreateMap<UserAdminDto, AdminUserListItemViewModel>().IgnoreNotExistingProperties();

            Mapper.CreateMap<WeatherDto, WeatherDataViewModel>()
                .ForMember(x => x.DateOfRead, opt => opt.MapFrom(src => src.DateOfRead.ToString("dd MMMM yyyy")))
                .ForMember(x => x.HourOfRead, opt => opt.MapFrom(src => src.DateOfRead.Hour))
                .IgnoreNotExistingProperties();

            Mapper.CreateMap<WeatherInfoDto, WeatherInfoDataViewModel>()
                .ForMember(x => x.WeatherId, src => src.MapFrom(a => a.WeatherConditionId))
                .IgnoreNotExistingProperties();
        }
    }
}
