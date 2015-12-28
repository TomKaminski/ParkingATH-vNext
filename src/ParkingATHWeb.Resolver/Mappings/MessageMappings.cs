using AutoMapper;
using ParkingATHWeb.Contracts.DTO;
using ParkingATHWeb.Model.Concrete;
using ParkingATHWeb.Shared.Helpers;

namespace ParkingATHWeb.Resolver.Mappings
{
    public static partial class BackendMappingProvider
    {
        private static void InitalizeMessageMappings
            ()
        {
            Mapper.CreateMap<Message, MessageDto>().IgnoreNotExistingProperties();

            Mapper.CreateMap<MessageDto, Message>().IgnoreNotExistingProperties();
        }
    }
}
