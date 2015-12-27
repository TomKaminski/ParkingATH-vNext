using System;
using System.Collections.Generic;
using System.IO;
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
using ParkingATHWeb.Shared.Enums;

namespace ParkingATHWeb.Business.Services
{
    public class MessageService : EntityService<MessageDto, Message, Guid>, IMessageService
    {
        private readonly ISmtpClient _smtpClient;
        private readonly IConfigurationRoot _configuration;
        private readonly IApplicationEnvironment _appEnv;

        private const string BodyMarker = "{{BodyHtml}}";

        public MessageService(IGenericRepository<Message, Guid> repository, IUnitOfWork unitOfWork,
            ISmtpClient smtpClient, IApplicationEnvironment appEnv) : base(repository, unitOfWork)
        {
            _smtpClient = smtpClient;
            _appEnv = appEnv;

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
            _smtpClient.UseDefaultCredentials =
                Convert.ToBoolean(_configuration["Settings:SmtpConfiguration:defaultCredentials"]);
            _smtpClient.Credentials = new NetworkCredential(_configuration["Settings:SmtpConfiguration:userName"],
                _configuration["Settings:SmtpConfiguration:password"]);
        }

        public void SendMessage(MessageDto message, Dictionary<string, string> parameters)
        {
            ConfigureSmtpSettings();
            var templateHtml = GetValidTemplateString(message.Type);
            templateHtml = PrepareEmailBody(templateHtml, parameters);
            var emailBody = InsertBodyIntoLayout(templateHtml);

            var mailMessage = new MailMessage(_configuration["Settings:SmtpConfiguration:from"], "tkaminski93@gmail.com",
                "Rejestracja w systemie Parking ATH", emailBody)
            {
                IsBodyHtml = true
            };
            _smtpClient.Send(mailMessage);
        }


        private string GetLayoutTemplate()
        {
            return File.ReadAllText(_appEnv.ApplicationBasePath + "/Content/Emails/_EmailLayout.html");
        }

        private string GetValidTemplateString(EmailType type)
        {
            switch (type)
            {
                case EmailType.Register:
                    return File.ReadAllText(_appEnv.ApplicationBasePath + "/Content/Emails/Register.html");
                case EmailType.ResetPassword:
                    break;
                case EmailType.ForgotPassword:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
            return string.Empty;
        }

        private string PrepareEmailBody(string template, Dictionary<string, string> parameters)
        {
            var localTemplate = template;
            foreach (var parameter in parameters)
            {
                localTemplate = template.Replace("{{" + parameter.Key + "}}", parameter.Value);
            }
            return localTemplate;
        }

        private string InsertBodyIntoLayout(string bodyHtml)
        {
            return GetLayoutTemplate().Replace(BodyMarker, bodyHtml);
        }
    }
}
