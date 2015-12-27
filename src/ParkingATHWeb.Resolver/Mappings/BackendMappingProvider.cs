using AutoMapper;
using ParkingATHWeb.Contracts.DTO;
using ParkingATHWeb.Model.Concrete;
using ParkingATHWeb.Shared.Helpers;

namespace ParkingATHWeb.Resolver.Mappings
{
    public static partial class BackendMappingProvider
    {
        public static void InitMappings()
        {
            BackendMappingProvider.InitializeOrderMappings();
            BackendMappingProvider.InitializeGateUsageMappings();
            BackendMappingProvider.InitializePriceTresholdMappings();
            BackendMappingProvider.InitializeStudentMappings();

            Mapper.CreateMap<Message,MessageDto>().IgnoreNotExistingProperties();
            Mapper.CreateMap<MessageDto, Message>().IgnoreNotExistingProperties();

        }
    }
}
