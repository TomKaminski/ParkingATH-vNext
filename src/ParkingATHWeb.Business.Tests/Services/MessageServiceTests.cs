using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mail.Abstractions;
using Autofac.Extras.Moq;
using Moq;
using ParkingATHWeb.Business.Providers.Email;
using ParkingATHWeb.Business.Services;
using ParkingATHWeb.Business.Tests.Base;
using ParkingATHWeb.Contracts.Common;
using ParkingATHWeb.Contracts.DTO;
using ParkingATHWeb.Contracts.DTO.Token;
using ParkingATHWeb.Contracts.Services;
using ParkingATHWeb.DataAccess;
using ParkingATHWeb.DataAccess.Common;
using ParkingATHWeb.DataAccess.Repositories;
using ParkingATHWeb.Resolver.Mappings;
using ParkingATHWeb.Shared.Enums;
using SharpTestsEx;
using Xunit;

namespace ParkingATHWeb.Business.Tests.Services
{
    public class MessageServiceTests : BusinessTestBase
    {
        private readonly MessageService _sut;
        private readonly AutoMock _mock = AutoMock.GetLoose();
        private readonly Mock<ISmtpClient> _smtpClientMock;
        private readonly IUnitOfWork _unitOfWork;
        private readonly MessageRepository _messageRepoMock;

        public MessageServiceTests()
        {
            BackendMappingProvider.InitMappings();
            _mock.Mock<IDatabaseFactory>().Setup(x => x.Get()).Returns(GetContext());
            _messageRepoMock = _mock.Create<MessageRepository>();
            _unitOfWork = _mock.Create<UnitOfWork>();

            _smtpClientMock = _mock.Mock<System.Net.Mail.Abstractions.ISmtpClient>();
            _smtpClientMock.Setup(x => x.SendMailAsync(It.IsAny<MailMessage>())).Verifiable();

            var appEnvMock = _mock.Mock<IAppSettingsProvider>();
            appEnvMock.Setup(x => x.GetSmtpSettings()).Returns(new SmtpSettings
            {
                Credentials = new NetworkCredential("parkingath@gmail.com", "fukme123"),
                SmtpDeliveryMethod = SmtpDeliveryMethod.Network,
                DeliveryFormat = SmtpDeliveryFormat.International,
                Port = 587,
                UseDefaultCredentials = false,
                EnableSsl = true,
                From = "parkingath@gmail.com",
                Host = "smtp.gmail.com"
            });

            var emailBodyProviderMock = new Mock<EmailContentProvider> { CallBase = true };
            emailBodyProviderMock.Setup(x => x.GetValidTemplateString(EmailType.ResetPassword)).Returns(EmailBodyChangeReset);
            emailBodyProviderMock.Setup(x => x.GetValidTemplateString(EmailType.Register)).Returns(EmailBodyRegister);
            emailBodyProviderMock.Setup(x => x.GetLayoutTemplate()).Returns(TestLayoutRegister);
            emailBodyProviderMock.Setup(x => x.GetEmailTitle(It.IsAny<EmailType>())).Returns("Email Title");

            var tokenServiceMock = new Mock<ITokenService>();
            tokenServiceMock.Setup(x => x.CreateAsync(It.IsAny<TokenType>())).ReturnsAsync(ServiceResult<TokenBaseDto>.Success(GetTokenDto(TokenType.EmailChangeToken)));

            _sut = new MessageService(_unitOfWork, _smtpClientMock.Object, appEnvMock.Object, emailBodyProviderMock.Object, _messageRepoMock, tokenServiceMock.Object);
        }

        [Fact]
        public async void GetMessageByTokenId_ThenResultIsValid()
        {
            //before
            var tokenService = new TokenService(_unitOfWork, _mock.Create<TokenRepository>());
            var tokenCreateResult = tokenService.Create(TokenType.ViewInBrowserToken);
            var messageDto = GetBaseMessageDto(EmailType.Register);
            messageDto.ViewInBrowserTokenId = tokenCreateResult.Result.Id;
            _sut.Create(messageDto);

            //act
            var result = await _sut.GetMessageByTokenId(tokenCreateResult.Result.Id);

            //then
            result.IsValid.Should().Be.True();
            result.Result.ViewInBrowserTokenId.Should().Be.EqualTo(tokenCreateResult.Result.Id);
        }

        [Fact]
        public async void SendMessageAsync_ThenResultIsValid()
        {
            //act
            var result = await _sut.SendMessageAsync(EmailType.Register, GetUserBaseDto(), "sadsasadasd");

            //then
            result.IsValid.Should().Be.True();
        }

        [Fact]
        public void WhenMessageDataIsProvider_GetMessageBody_ThenResultIsValid()
        {
            //Before
            var dto = _sut.Create(GetBaseMessageDto(EmailType.ResetPassword));
            
            //act
            var messageBodyResult = _sut.GetMessageBody(dto.Result);

            //act
            messageBodyResult.IsValid.Should().Be.True();
            messageBodyResult.Result.Should().Be.EqualTo(FullEmailChangeReset);
        }
    }
}
