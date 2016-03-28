using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using ParkingATHWeb.Areas.Admin.ViewModels.User;
using ParkingATHWeb.Areas.Portal.ViewModels.Chart;
using ParkingATHWeb.Areas.Portal.ViewModels.GateUsage;
using ParkingATHWeb.Areas.Portal.ViewModels.Message;
using ParkingATHWeb.Areas.Portal.ViewModels.Payment;
using ParkingATHWeb.Areas.Portal.ViewModels.PortalMessage;
using ParkingATHWeb.Areas.Portal.ViewModels.PriceTreshold;
using ParkingATHWeb.Areas.Portal.ViewModels.User;
using ParkingATHWeb.Areas.Portal.ViewModels.Weather;
using ParkingATHWeb.Contracts.DTO;
using ParkingATHWeb.Contracts.DTO.Chart;
using ParkingATHWeb.Contracts.DTO.GateUsage;
using ParkingATHWeb.Contracts.DTO.Order;
using ParkingATHWeb.Contracts.DTO.Payments;
using ParkingATHWeb.Contracts.DTO.PortalMessage;
using ParkingATHWeb.Contracts.DTO.PriceTreshold;
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

            CreateMap<ReplyMessageViewModel, PortalMessageDto>().IgnoreNotExistingProperties();

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
                .ForMember(x => x.CreateDate, opt => opt.MapFrom(a => a.CreateDate.ToString("dd.MM.yy HH:mm")))
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

            CreateMap<PriceTresholdBaseDto, PriceTresholdShopItemViewModel>()
                .ForMember(x => x.PriceLabel, a => a.MapFrom(s => s.PricePerCharge.ToString("#.00")))
                .IgnoreNotExistingProperties();

            CreateMap<OrderBaseDto, ShopOrderItemViewModel>()
                .ForMember(x => x.Price, a => a.MapFrom(s => s.Price.ToString("#.00")))
                .ForMember(x => x.PricePerCharge, a => a.MapFrom(s => s.PricePerCharge.ToString("#.00")))
                .ForMember(x => x.Date, a => a.MapFrom(s => s.Date.ToString("dd.MM.yyyy")))
                .ForMember(x => x.Time, a => a.MapFrom(s => s.Date.ToString("HH:mm")))
                .AfterMap((src, dest) =>
                {
                    switch (src.OrderState)
                    {
                        case OrderStatus.Completed:
                            dest.OrderState = "Sfinalizowane";
                            dest.OrderStateStyle = "order-success";
                            break;
                        case OrderStatus.Canceled:
                            dest.OrderState = "Anulowane";
                            dest.OrderStateStyle = "order-canceled";
                            break;
                        case OrderStatus.Rejected:
                            dest.OrderState = "Odrzucone";
                            dest.OrderStateStyle = "order-rejected";
                            break;
                        case OrderStatus.Pending:
                            dest.OrderState = "Oczekujące";
                            dest.OrderStateStyle = "order-pending";
                            break;
                    }
                    switch (src.OrderPlace)
                    {
                        case OrderPlace.Panel:
                            dest.OrderPlace = "Panel zakupowy";
                            break;
                        case OrderPlace.Website:
                            dest.OrderPlace = "Portal";
                            break;
                    }
                }).IgnoreNotExistingProperties();

            CreateMap<PaymentRequestViewModel, PaymentRequest>()
                .ForMember(x => x.buyer, a => a.MapFrom(s => new Contracts.DTO.Payments.Buyer
                {
                    email = s.UserEmail,
                    firstName = s.UserName,
                    lastName = s.UserLastName
                }))
                .ForMember(x=>x.description, a=>a.MapFrom(s=>$"Zakup wyjazdów w SmartPark - {s.Charges}x"))
                .ForMember(x => x.products, s => s.MapFrom(a => new List<Contracts.DTO.Payments.Product>
                {
                    new Contracts.DTO.Payments.Product
                    {
                        name = $"SmartPark - wyjazdy - {a.Charges}x",
                        quantity = a.Charges.ToString(),
                    }
                }))
                .ForMember(x => x.customerIp, a => a.MapFrom(s => s.CustomerIP)).IgnoreNotExistingProperties();

            CreateMap<PaymentResponse,PaymentResponseViewModel>()
                .ForMember(x=>x.RedirectUri, a=>a.MapFrom(s=>s.redirectUri)).IgnoreNotExistingProperties();

            CreateMap<GateUsageBaseDto,GateOpeningViewModel>()
                .ForMember(x=>x.Date, s=>s.MapFrom(a=>a.DateOfUse.ToString("dd MMMM yyyy")))
                .ForMember(x => x.Date, s => s.MapFrom(a => a.DateOfUse.ToString("HH:mm")))
                .IgnoreNotExistingProperties();
        }


    }
}
