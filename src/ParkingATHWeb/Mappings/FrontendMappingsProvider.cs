using AutoMapper;
using ParkingATHWeb.Contracts.DTO;
using ParkingATHWeb.Models;
using ParkingATHWeb.Shared.Helpers;

namespace ParkingATHWeb.Mappings
{
    public static partial class FrontendMappingsProvider
    {
        public static void InitMappings()
        {
            InitAccountMappings();

            Mapper.CreateMap<ParkingAthMessage, MessageDto>().IgnoreNotExistingProperties();
            Mapper.CreateMap<MessageDto, ParkingAthMessage>().IgnoreNotExistingProperties();
        }
    }

}
