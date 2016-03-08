using System;
using AutoMapper;
using ParkingATHWeb.Contracts.DTO.SupportMessage;
using ParkingATHWeb.Model.Concrete;
using ParkingATHWeb.Shared.Enums;
using ParkingATHWeb.Shared.Helpers;

namespace ParkingATHWeb.Resolver.Mappings
{
    public class PortalMessageBackendMappings : Profile
    {
        protected override void Configure()
        {
            CreateMap<PortalMessageDto, PortalMessage>()
                .ForMember(x => x.PortalMessageType, a => a.MapFrom(s => Convert.ToInt32(s.PortalMessageType)))
                .ForMember(x => x.Title, a => a.UseValue($"Wiadomość do działu pomocy - {DateTime.Today.ToString("dd.MM.yyyy")} - {DateTime.Today.TimeOfDay.Hours}{DateTime.Today.TimeOfDay.Minutes}{DateTime.Today.TimeOfDay.Seconds}{DateTime.Today.TimeOfDay.Milliseconds}"))
                .IgnoreNotExistingProperties();

            CreateMap<PortalMessage, PortalMessageDto>()
                .ForMember(x => x.PortalMessageType, a => a.MapFrom(s => (PortalMessageEnum)s.PortalMessageType))
                .IgnoreNotExistingProperties();
        }
    }
}
