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
    public class FrontendMappings : Profile
    {
        protected override void Configure()
        {
            CreateMap<ParkingAthMessage, MessageDto>().IgnoreNotExistingProperties();
            CreateMap<MessageDto, ParkingAthMessage>().IgnoreNotExistingProperties();

            CreateMap<UserBaseDto, UserBaseViewModel>().IgnoreNotExistingProperties();
            CreateMap<UserAdminDto, AdminUserListItemViewModel>().IgnoreNotExistingProperties();

            CreateMap<WeatherDto, WeatherDataViewModel>()
                .ForMember(x => x.DateOfRead, opt => opt.MapFrom(src => src.DateOfRead.ToString("dd MMMM yyyy")))
                .ForMember(x => x.HourOfRead, opt => opt.MapFrom(src => src.DateOfRead.Hour))
                .IgnoreNotExistingProperties();

            CreateMap<WeatherInfoDto, WeatherInfoDataViewModel>()
                .ForMember(x => x.WeatherId, src => src.MapFrom(a => a.WeatherConditionId))
                .IgnoreNotExistingProperties();
        }
    }
}
