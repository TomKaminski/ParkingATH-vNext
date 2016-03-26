using System.Linq;
using AutoMapper;
using ParkingATHWeb.Contracts.DTO.User;
using ParkingATHWeb.Contracts.DTO.UserPreferences;
using ParkingATHWeb.Model.Concrete;
using ParkingATHWeb.Shared.Helpers;

namespace ParkingATHWeb.Resolver.Mappings
{
    public class UserBackendMappings : Profile
    {
        protected override void Configure()
        {
            CreateMap<User, UserBaseDto>().IgnoreNotExistingProperties();
            CreateMap<UserBaseDto, User>().IgnoreNotExistingProperties();

            CreateMap<User, UserAdminDto>()
                .ForMember(x => x.OrdersCount, opt => opt.MapFrom(x => x.Orders.Count))
                .ForMember(x => x.GateUsagesCount, opt => opt.MapFrom(x => x.GateUsages.Count))
                .ForMember(x => x.ImgId, opt => opt.MapFrom(a => a.UserPreferences.ProfilePhotoId.ToString()))
                .ForMember(x => x.Orders, opt => opt.MapFrom(a => a.Orders.OrderByDescending(x=>x.Date).Take(3)))
                .IgnoreNotExistingProperties();

            CreateMap<UserPreferences, UserPreferencesDto>().IgnoreNotExistingProperties();
            CreateMap<UserPreferencesDto, UserPreferences>().IgnoreNotExistingProperties();
        }
    }
}
