using System.Linq;
using AutoMapper;
using ParkingATHWeb.Areas.Admin.ViewModels.User;
using ParkingATHWeb.Areas.Portal.ViewModels.Chart;
using ParkingATHWeb.Areas.Portal.ViewModels.Message;
using ParkingATHWeb.Areas.Portal.ViewModels.PortalMessage;
using ParkingATHWeb.Areas.Portal.ViewModels.User;
using ParkingATHWeb.Areas.Portal.ViewModels.Weather;
using ParkingATHWeb.Contracts.DTO;
using ParkingATHWeb.Contracts.DTO.Chart;
using ParkingATHWeb.Contracts.DTO.PortalMessage;
using ParkingATHWeb.Contracts.DTO.User;
using ParkingATHWeb.Contracts.DTO.UserPreferences;
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

            CreateMap<ChartDataRequest, ChartRequestDto>()
                .ForMember(x => x.DateRange, opt => opt.MapFrom(x => new DateRange(x.StartDate, x.EndDate)))
                .IgnoreNotExistingProperties();

            CreateMap<ChartDataRequest, UserPreferenceChartSettingsDto>().IgnoreNotExistingProperties();

            CreateMap<ChartRequestDto, ChartPreferencesReturnModel>()
                .ForMember(x => x.StartDate, opt => opt.MapFrom(a => a.DateRange.StartDate))
                .ForMember(x => x.EndDate, opt => opt.MapFrom(a => a.DateRange.EndDate))
                .ForMember(x => x.LabelStartDate, opt => opt.MapFrom(a => a.DateRange.StartDate.ToString("dd-MM-yy")))
                .ForMember(x => x.LabelEndDate, opt => opt.MapFrom(a => a.DateRange.EndDate.ToString("dd-MM-yy"))).IgnoreNotExistingProperties();


            CreateMap<ChartListDto, ChartDataReturnModel>()
                .ForMember(dest => dest.Labels, opt => opt.MapFrom(x => x.Elements.Select(el => el.DateLabel).ToArray()))
                .ForMember(dest => dest.Data, opt => opt.MapFrom(x => x.Elements.Select(el => el.NodeValue).ToArray()))
                .IgnoreNotExistingProperties();

            CreateMap<PortalMessageDto, PortalMessageItemViewModel>()
                .ForMember(x => x.CreateDate, opt => opt.MapFrom(a => a.CreateDate.ToString("dd.MM.yy hh:mm")))
                .IgnoreNotExistingProperties();

            CreateMap<PortalMessageUserDto, PortalMessageUserViewModel>()
                .ForMember(x => x.IsDeleted, opt => opt.MapFrom(a => a.Id == 0))
                .IgnoreNotExistingProperties();

            CreateMap<PortalMessageClusterDto, PortalMessageClusterViewModel>()
                .ForMember(x => x.ReceiverUser, opt => opt.MapFrom(a => a.ReceiverUser))
                .ForMember(x => x.Messages, opt => opt.MapFrom(a => a.Cluster))
                .IgnoreNotExistingProperties();

            CreateMap<PortalMessageClustersDto, PortalMessageClustersViewModel>()
                .ForMember(x => x.User, opt => opt.MapFrom(a => a.User))
                .ForMember(x => x.Clusters, opt => opt.MapFrom(a => a.Clusters))
                .IgnoreNotExistingProperties();
        }
    }
}
