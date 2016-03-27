using AutoMapper;
using ParkingATHWeb.Areas.Admin.ViewModels.GateUsage;
using ParkingATHWeb.Areas.Admin.ViewModels.Order;
using ParkingATHWeb.Areas.Admin.ViewModels.PriceTreshold;
using ParkingATHWeb.Areas.Admin.ViewModels.User;
using ParkingATHWeb.Contracts.DTO.GateUsage;
using ParkingATHWeb.Contracts.DTO.Order;
using ParkingATHWeb.Contracts.DTO.PriceTreshold;
using ParkingATHWeb.Contracts.DTO.User;
using ParkingATHWeb.Shared.Helpers;

namespace ParkingATHWeb.Mappings
{
    public class AdminMappingsProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<GateUsageAdminDto, AdminGateUsageListItemViewModel>().IgnoreNotExistingProperties();
            CreateMap<OrderAdminDto, AdminOrderListItemViewModel>().IgnoreNotExistingProperties();
            CreateMap<PriceTresholdAdminDto, AdminPriceTresholdListItemViewModel>().IgnoreNotExistingProperties();
            CreateMap<UserAdminDto, AdminUserListItemViewModel>()
                .ForMember(x=>x.Initials, a=>a.MapFrom(s=>$"{s.Name} {s.LastName}"))
                .ForMember(x=>x.CreateDateLabel, a=>a.MapFrom(s=>s.CreateDate.ToString("dd-MM-yyyy hh:mm")))
                .ForMember(x => x.LastUserOrders, a => a.MapFrom(s=>s.Orders))
                .IgnoreNotExistingProperties();


            CreateMap<AdminUserEditViewModel,UserBaseDto>().IgnoreNotExistingProperties();
            CreateMap<AdminPriceTresholdCreateViewModel, PriceTresholdBaseDto>().IgnoreNotExistingProperties();
            CreateMap<AdminPriceTresholdEditViewModel, PriceTresholdBaseDto>().IgnoreNotExistingProperties();

        }
    }
}
