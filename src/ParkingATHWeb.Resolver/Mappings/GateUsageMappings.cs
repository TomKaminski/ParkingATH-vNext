﻿using AutoMapper;
using ParkingATHWeb.Contracts.DTO.GateUsage;
using ParkingATHWeb.Model.Concrete;
using ParkingATHWeb.Shared.Helpers;

namespace ParkingATHWeb.Resolver.Mappings
{
    public static partial class BackendMappingProvider
    {
        private static void InitializeGateUsageMappings()
        {

            Mapper.CreateMap<GateUsage, GateUsageBaseDto>().IgnoreNotExistingProperties();

            Mapper.CreateMap<GateUsage, GateUsageAdminDto>()
                .ForMember(x => x.Initials, opt => opt.MapFrom(k => $"{k.User.Name} {k.User.LastName}"))
                .IgnoreNotExistingProperties();

            Mapper.CreateMap<GateUsageBaseDto, GateUsage>().IgnoreNotExistingProperties();
        }
    }
}
