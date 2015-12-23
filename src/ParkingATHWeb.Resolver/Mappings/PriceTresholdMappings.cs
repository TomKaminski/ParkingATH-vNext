using AutoMapper;
using ParkingATHWeb.Contracts.DTO.PriceTreshold;
using ParkingATHWeb.Model.Concrete;
using ParkingATHWeb.Shared.Helpers;

namespace ParkingATHWeb.Resolver.Mappings
{
    public static partial class BackendMappingProvider
    {
        private static void InitializePriceTresholdMappings()
        {
            Mapper.CreateMap<PriceTreshold, PriceTresholdBaseDto>().IgnoreNotExistingProperties();

            Mapper.CreateMap<PriceTreshold, PriceTresholdAdminDto>()
                .ForMember(x => x.NumOfOrders, opt => opt.MapFrom(k => k.Orders.Count))
                .IgnoreNotExistingProperties();

            Mapper.CreateMap<PriceTresholdBaseDto, PriceTreshold>().IgnoreNotExistingProperties();
        }
    }
}
