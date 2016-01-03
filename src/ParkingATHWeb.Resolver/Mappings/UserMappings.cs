using AutoMapper;
using ParkingATHWeb.Contracts.DTO.User;
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
        }
    }
}
