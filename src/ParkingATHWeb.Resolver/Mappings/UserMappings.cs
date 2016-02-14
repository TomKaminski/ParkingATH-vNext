using AutoMapper;
using ParkingATHWeb.Contracts.DTO.User;
using ParkingATHWeb.Contracts.DTO.UserPreferences;
using ParkingATHWeb.Model.Concrete;
using ParkingATHWeb.Shared.Helpers;

namespace ParkingATHWeb.Resolver.Mappings
{
    public static partial class BackendMappingProvider
    {
        private static void InitializeStudentMappings()
        {
            Mapper.CreateMap<User, UserBaseDto>().IgnoreNotExistingProperties();
            Mapper.CreateMap<UserBaseDto, User>().IgnoreNotExistingProperties();

            Mapper.CreateMap<User, UserAdminDto>()
                .ForMember(x=>x.OrdersCount, opt=>opt.MapFrom(x=>x.Orders.Count))
                .IgnoreNotExistingProperties();

            Mapper.CreateMap<UserPreferences, UserPreferencesDto>().IgnoreNotExistingProperties();
            Mapper.CreateMap<UserPreferencesDto, UserPreferences>().IgnoreNotExistingProperties();
        }
    }
}
