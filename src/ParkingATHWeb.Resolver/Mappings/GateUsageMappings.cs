using AutoMapper;
using ParkingATHWeb.Contracts.DTO.GateUsage;
using ParkingATHWeb.Model.Concrete;
using ParkingATHWeb.Shared.Helpers;

namespace ParkingATHWeb.Resolver.Mappings
{
    public class GateUsageBackendMappings : Profile
    {
        protected override void Configure()
        {
            CreateMap<GateUsage, GateUsageBaseDto>().IgnoreNotExistingProperties();

            CreateMap<GateUsage, GateUsageAdminDto>()
                .ForMember(x => x.Initials, opt => opt.MapFrom(k => $"{k.User.Name} {k.User.LastName}"))
                .IgnoreNotExistingProperties();

            CreateMap<GateUsageBaseDto, GateUsage>().IgnoreNotExistingProperties();
        }
    }
}
