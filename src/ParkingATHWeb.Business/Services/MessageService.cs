using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Net.Mail.Abstractions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ParkingATHWeb.Business.Providers.Email;
using ParkingATHWeb.Business.Services.Base;
using ParkingATHWeb.Contracts.Common;
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
        private readonly IEmailContentProvider _emailContentProvider;
        private readonly IMessageRepository _messageRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;

        private readonly SmtpSettings _smtpSettings;

        private const string TokenRedirectFormat = "/Redirect?id={0}";

        public MessageService(IUnitOfWork unitOfWork,
            ISmtpClient smtpClient, IAppSettingsProvider appSettingsProvider, IEmailContentProvider emailContentProvider, IMessageRepository messageRepository, ITokenService tokenService) : base(messageRepository, unitOfWork)
        {
            _smtpSettings = appSettingsProvider.GetSmtpSettings();
            _smtpClient = smtpClient;
            _smtpClient = Mapper.Map<System.Net.Mail.Abstractions.SmtpClient>(_smtpSettings);

            _emailContentProvider = emailContentProvider;
            _messageRepository = messageRepository;
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;

            _configuration = appSettingsProvider.GetAppSettings(AppSettingsType.DefaultSettings, AppSettingsType.Resources);
        }

        public async Task<ServiceResult> SendMessageAsync(MessageDto message, UserBaseDto userData, string appBasePath)
        {
            var tokenCreateResult = await _tokenService.CreateAsync(TokenType.ViewInBrowserToken);

            var emailParameters = EmailParametersProvider.GetBaseParametersForEmail(userData);
            emailParameters.Add("ViewInBrowserLink", string.Format(appBasePath + TokenRedirectFormat, tokenCreateResult.Result.BuildEncryptedToken()));

            var emailBody = _emailContentProvider.GetEmailBody(message.Type, emailParameters);
            message.To = userData.Email;
            message.DisplayFrom = "Parking ATH";
            message.Title = _emailContentProvider.GetEmailTitle(message.Type);
            message.MessageParameters = JsonConvert.SerializeObject(emailParameters);
            message.UserId = userData.Id;
            message.From = _smtpSettings.From;
            message.ViewInBrowserTokenId = tokenCreateResult.Result.Id;

            _messageRepository.Add(Mapper.Map<Message>(message));
            await _unitOfWork.CommitAsync();

            var mailMessage = new MailMessage(message.From, userData.Email, message.Title, emailBody)
            {
                IsBodyHtml = true
            };
            await _smtpClient.SendMailAsync(mailMessage);
            return ServiceResult.Success();
        }

        public ServiceResult<string> GetMessageBody(MessageDto message)
        {
            return ServiceResult<string>.Success(_emailContentProvider.GetEmailBody(message.Type, JsonConvert.DeserializeObject<Dictionary<string, string>>(message.MessageParameters)));
        }

        public async Task<ServiceResult<MessageDto>> GetMessageByTokenId(long id)
        {
            return ServiceResult<MessageDto>.Success(Mapper.Map<MessageDto>(await _messageRepository.GetMessageByTokenId(id)));
        }
    }
}
