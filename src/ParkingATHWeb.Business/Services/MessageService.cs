using System;
using System.Net;
using System.Net.Mail;
using System.Net.Mail.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.PlatformAbstractions;
using ParkingATHWeb.Business.Services.Base;
using ParkingATHWeb.Contracts.DTO;
using ParkingATHWeb.Contracts.Services;
using ParkingATHWeb.DataAccess.Common;
using ParkingATHWeb.Model.Concrete;

namespace ParkingATHWeb.Business.Services
{
    public class MessageService:EntityService<MessageDto,Message,Guid>,IMessageService
    {
        private readonly ISmtpClient _smtpClient;
        private readonly IConfigurationRoot _configuration;
        private readonly IApplicationEnvironment _apppEnv;

        public MessageService(IGenericRepository<Message, Guid> repository, IUnitOfWork unitOfWork, ISmtpClient smtpClient, IApplicationEnvironment appEnv) : base(repository, unitOfWork)
        {
            _smtpClient = smtpClient;
            _apppEnv = appEnv;

            var builder = new ConfigurationBuilder()
              .SetBasePath(appEnv.ApplicationBasePath)
              .AddJsonFile("appsettings.json");

            _configuration = builder.Build();
        }

        private void ConfigureSmtpSettings()
        {
            _smtpClient.DeliveryFormat = SmtpDeliveryFormat.International;
            _smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            _smtpClient.EnableSsl = Convert.ToBoolean(_configuration["Settings:SmtpConfiguration:enableSsl"]);
            _smtpClient.Host = _configuration["Settings:SmtpConfiguration:host"];
            _smtpClient.Port = Convert.ToInt32(_configuration["Settings:SmtpConfiguration:port"]);
            _smtpClient.UseDefaultCredentials = Convert.ToBoolean(_configuration["Settings:SmtpConfiguration:defaultCredentials"]);
            _smtpClient.Credentials = new NetworkCredential(_configuration["Settings:SmtpConfiguration:userName"], _configuration["Settings:SmtpConfiguration:password"]);
        }

        public void SendMessage(MessageDto message)
        {
            ConfigureSmtpSettings();
            //TODO: email template from html file
            var mailMessage = new MailMessage(_configuration["Settings:SmtpConfiguration:from"], "tkaminski93@gmail.com", "Zalogowałeś się haha", "zalogowałeś się <b>BOLD</b>")
            {
                IsBodyHtml = true
            };
            _smtpClient.Send(mailMessage);
        }

       
    }
}
