using System;
using System.Net;
using System.Net.Mail;
using System.Net.Mail.Abstractions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.PlatformAbstractions;
using Newtonsoft.Json;
using ParkingATHWeb.Business.Providers.Email;
using ParkingATHWeb.Business.Services.Base;
using ParkingATHWeb.Contracts.DTO;
using ParkingATHWeb.Contracts.DTO.User;
using ParkingATHWeb.Contracts.Services;
using ParkingATHWeb.DataAccess.Common;
using ParkingATHWeb.DataAccess.Interfaces;
using ParkingATHWeb.Model.Concrete;
using ParkingATHWeb.Shared.Enums;

namespace ParkingATHWeb.Business.Services
{
    public class MessageService : EntityService<MessageDto, Message, Guid>, IMessageService
    {
        private readonly ISmtpClient _smtpClient;
        private readonly IConfigurationRoot _configuration;
        private readonly IEmailBodyProvider _emailBodyProvider;
        private readonly IMessageRepository _messageRepository;
        private readonly IUnitOfWork _unitOfWork;

        public MessageService(IUnitOfWork unitOfWork,
            ISmtpClient smtpClient, IApplicationEnvironment appEnv, IEmailBodyProvider emailBodyProvider, IMessageRepository messageRepository) : base(messageRepository, unitOfWork)
        {
            _smtpClient = smtpClient;
            _emailBodyProvider = emailBodyProvider;
            _messageRepository = messageRepository;
            _unitOfWork = unitOfWork;

            var builder = new ConfigurationBuilder()
                .SetBasePath(appEnv.ApplicationBasePath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile("Resources.json");

            _configuration = builder.Build();
        }

        private void ConfigureSmtpSettings()
        {
            _smtpClient.DeliveryFormat = SmtpDeliveryFormat.International;
            _smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            _smtpClient.EnableSsl = Convert.ToBoolean(_configuration["Settings:SmtpConfiguration:enableSsl"]);
            _smtpClient.Host = _configuration["Settings:SmtpConfiguration:host"];
            _smtpClient.Port = Convert.ToInt32(_configuration["Settings:SmtpConfiguration:port"]);
            _smtpClient.UseDefaultCredentials =
                Convert.ToBoolean(_configuration["Settings:SmtpConfiguration:defaultCredentials"]);
            _smtpClient.Credentials = new NetworkCredential(_configuration["Settings:SmtpConfiguration:userName"],
                _configuration["Settings:SmtpConfiguration:password"]);
        }

        public async Task SendMessageAsync(MessageDto message, UserBaseDto userData)
        {
            ConfigureSmtpSettings();
            var emailParameters = EmailParametersProvider.GetBaseParametersForEmail(userData);
            var emailBody = _emailBodyProvider.GetEmailBody(message.Type, userData, emailParameters);

            message.To = userData.Email;
            message.DisplayFrom = "Parking ATH";
            message.Title = GetEmailTitle(message.Type);
            message.MessageParameters = JsonConvert.SerializeObject(emailParameters);
            message.UserId = userData.Id;
            message.From = _configuration["Settings:SmtpConfiguration:from"];

            _messageRepository.Add(Mapper.Map<Message>(message));
            await _unitOfWork.CommitAsync();

            var mailMessage = new MailMessage(message.From, userData.Email, message.Title, emailBody)
            {
                IsBodyHtml = true
            };
            await _smtpClient.SendMailAsync(mailMessage);
        }


        private string GetEmailTitle(EmailType type)
        {
            switch (type)
            {
                case EmailType.Register:
                    return _configuration["EmailResources:RegisterEmail_Title"];
                case EmailType.ResetPassword:
                    return _configuration["EmailResources:ChangePassword_Title"];
                case EmailType.ChangePassword:
                    return _configuration["EmailResources:ResetPassword_Title"];
            }
            throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }
}
