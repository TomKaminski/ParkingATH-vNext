using System;
using System.Net.Mail.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.PlatformAbstractions;
using ParkingATHWeb.Contracts.DTO;
using ParkingATHWeb.Contracts.Services;

namespace ParkingATHWeb.Business.Services
{
    public class MessageService:IMessageService
    {
        private ISmtpClient _smtpClient;
        private IConfigurationRoot Configuration { get; set; }

        public MessageService(ISmtpClient smtpClient, IApplicationEnvironment appEnv)
        {
            _smtpClient = smtpClient;
            var builder = new ConfigurationBuilder()
              .SetBasePath(appEnv.ApplicationBasePath)
              .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
        }

        private void ConfigureSmtpSettings()
        {
            //TODO: configure smtp client
            var plz = Configuration["Settings:SmtpConfiguration:smtp"];
        }

        public void SendMessage(MessageDto message)
        {
            ConfigureSmtpSettings();
            //TODO: send message
        }
    }
}
