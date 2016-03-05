using System.Linq;
using AutoMapper;
using Microsoft.AspNet.Mvc.Rendering;
using ParkingATHWeb.Areas.Admin.ViewModels.User;
using ParkingATHWeb.Areas.Portal.ViewModels.Chart;
using ParkingATHWeb.Areas.Portal.ViewModels.Message;
using ParkingATHWeb.Areas.Portal.ViewModels.User;
using ParkingATHWeb.Areas.Portal.ViewModels.Weather;
using ParkingATHWeb.Contracts.DTO;
using ParkingATHWeb.Contracts.DTO.Chart;
using ParkingATHWeb.Contracts.DTO.SupportMessage;
using ParkingATHWeb.Contracts.DTO.User;
using ParkingATHWeb.Contracts.DTO.Weather;
using ParkingATHWeb.Contracts.DTO.WeatherInfo;
using ParkingATHWeb.Models;
using ParkingATHWeb.Shared.Enums;
using ParkingATHWeb.Shared.Helpers;

namespace ParkingATHWeb.Mappings
{
    public class FrontendMappings : Profile
    {
        protected override void Configure()
        {
            CreateMap<ParkingAthMessage, MessageDto>().IgnoreNotExistingProperties();
            CreateMap<MessageDto, ParkingAthMessage>().IgnoreNotExistingProperties();

            CreateMap<UserBaseDto, UserBaseViewModel>()
                .ForMember(x => x.CreateDate, opt => opt.MapFrom(src => src.CreateDate.ToLongDateString()))
                .ForMember(x => x.Range, src => src.MapFrom(y => y.IsAdmin ? "Administrator" : "Użytkownik"))
                .IgnoreNotExistingProperties();

            CreateMap<UserAdminDto, AdminUserListItemViewModel>().IgnoreNotExistingProperties();

            CreateMap<WeatherDto, WeatherDataViewModel>()
                .ForMember(x => x.DateOfRead, opt => opt.MapFrom(src => src.DateOfRead.ToString("dd MMMM yyyy")))
                .ForMember(x => x.HourOfRead, opt => opt.MapFrom(src => src.DateOfRead.Hour))
                .IgnoreNotExistingProperties();

            CreateMap<WeatherInfoDto, WeatherInfoDataViewModel>()
                .ForMember(x => x.WeatherId, src => src.MapFrom(a => a.WeatherConditionId))
                .IgnoreNotExistingProperties();

            CreateMap<QuickMessageViewModel, PortalMessageDto>()
                .ForMember(x => x.PortalMessageType, opt => opt.UseValue(PortalMessageEnum.MessageToAdminFromUser))
                .IgnoreNotExistingProperties();

            CreateMap<ChartDataRequest,ChartRequestDto>()
                .ForMember(x=>x.DateRange, opt=>opt.MapFrom(x=> new DateRange(x.StartDate,x.EndDate)))
                .IgnoreNotExistingProperties();

            CreateMap<ChartListDto, ChartDataReturnModel>()
                .ForMember(dest => dest.Labels,opt=>opt.MapFrom(x=>x.Elements.Select(el => el.DateLabel).ToArray()))
                .ForMember(dest => dest.Data, opt => opt.MapFrom(x => x.Elements.Select(el => el.NodeValue).ToArray()))
                .IgnoreNotExistingProperties();
        }
    }
}
