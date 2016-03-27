using AutoMapper;
using ParkingATHWeb.Areas.Admin.ViewModels.GateUsage;
using ParkingATHWeb.Areas.Admin.ViewModels.Order;
using ParkingATHWeb.Areas.Admin.ViewModels.PriceTreshold;
using ParkingATHWeb.Areas.Admin.ViewModels.User;
using ParkingATHWeb.Contracts.DTO.GateUsage;
using ParkingATHWeb.Contracts.DTO.Order;
using ParkingATHWeb.Contracts.DTO.PriceTreshold;
using ParkingATHWeb.Contracts.DTO.User;
using ParkingATHWeb.Shared.Enums;
using ParkingATHWeb.Shared.Helpers;

namespace ParkingATHWeb.Mappings
{
    public class AdminMappingsProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<GateUsageAdminDto, AdminGateUsageListItemViewModel>().IgnoreNotExistingProperties();
            CreateMap<PriceTresholdAdminDto, AdminPriceTresholdListItemViewModel>().IgnoreNotExistingProperties();
            CreateMap<UserAdminDto, AdminUserListItemViewModel>()
                .ForMember(x=>x.Initials, a=>a.MapFrom(s=>$"{s.Name} {s.LastName}"))
                .ForMember(x=>x.CreateDateLabel, a=>a.MapFrom(s=>s.CreateDate.ToString("dd-MM-yyyy hh:mm")))
                .ForMember(x => x.LastUserOrders, a => a.MapFrom(s=>s.Orders))
                .IgnoreNotExistingProperties();


            CreateMap<AdminUserEditViewModel,UserBaseDto>().IgnoreNotExistingProperties();
            CreateMap<AdminPriceTresholdCreateViewModel, PriceTresholdBaseDto>().IgnoreNotExistingProperties();
            CreateMap<AdminPriceTresholdEditViewModel, PriceTresholdBaseDto>().IgnoreNotExistingProperties();


            CreateMap<OrderAdminDto, AdminOrderListItemViewModel>()
               .ForMember(x => x.Price, a => a.MapFrom(s => s.Price.ToString("#.00")))
               .ForMember(x => x.PricePerCharge, a => a.MapFrom(s => s.PricePerCharge.ToString("#.00")))
               .ForMember(x => x.Date, a => a.MapFrom(s => s.Date.ToString("dd.MM.yyyy")))
               .ForMember(x => x.Time, a => a.MapFrom(s => s.Date.ToString("HH:mm")))
               .ForMember(x=>x.Initials, a=>a.MapFrom(s=>$"{s.Name} {s.LastName}"))
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
        }
    }
}
