using System.Net.Mail.Abstractions;
using AutoMapper;
using ParkingATHWeb.Contracts.DTO;
using ParkingATHWeb.Model.Concrete;
using ParkingATHWeb.Shared.Helpers;

namespace ParkingATHWeb.Resolver.Mappings
{
    public class MessageBackendMappings : Profile
    {
        protected override void Configure()
        {
            CreateMap<Message, MessageDto>().IgnoreNotExistingProperties();
            CreateMap<MessageDto, Message>().IgnoreNotExistingProperties();
            CreateMap<SmtpSettings, SmtpClient>()
                .AfterMap((src, dest) =>
                {
                    dest.Credentials = src.Credentials;
                }).IgnoreNotExistingProperties();
        }
    }
}
