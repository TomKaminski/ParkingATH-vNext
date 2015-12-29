using System.Collections.Generic;
using Autofac.Extras.Moq;
using Moq;
using ParkingATHWeb.Business.Providers.Email;
using ParkingATHWeb.Shared.Enums;
using SharpTestsEx;
using Xunit;

namespace ParkingATHWeb.Business.Tests.Providers
{
    public class EmailBodyProviderTests
    {
        private const string TestLayoutRegister = @"<div><h1>Email layout {{TekstLayoutPlz}}</h1><p>{{UserName}}</p><br/><br/><div>{{BodyHtml}}</div></div>";
        private const string EmailBodyRegister = @"<div><h3>Email boooody Register</h3></div>";
        private const string FullEmailRegister = @"<div><h1>Email layout Hahahahahahaha</h1><p>Tomek Kamiński</p><br/><br/><div><div><h3>Email boooody Register</h3></div></div></div>";

        private const string EmailBodyChangeReset = @"<div><h3>Email boooody ChangeReset</h3></div>";
        private const string FullEmailChangeReset = @"<div><h1>Email layout Hahahahahahaha</h1><p>Tomek Kamiński</p><br/><br/><div><div><h3>Email boooody ChangeReset</h3></div></div></div>";


        private readonly Dictionary<string, string> _customParameters = new Dictionary<string, string>
        {
            { "UserName", "Tomek Kamiński" },
            { "TekstLayoutPlz", "Hahahahahahaha" }
        };

        private readonly Mock<EmailBodyProvider> _sut;
        private readonly AutoMock _mock = AutoMock.GetLoose();

        public EmailBodyProviderTests()
        {
            //Before
            _sut = new Mock<EmailBodyProvider> {CallBase = true};
            _sut.Setup(x => x.GetValidTemplateString(EmailType.ResetPassword)).Returns(EmailBodyChangeReset);
            _sut.Setup(x => x.GetValidTemplateString(EmailType.ChangePassword)).Returns(EmailBodyChangeReset);
            _sut.Setup(x => x.GetValidTemplateString(EmailType.Register)).Returns(EmailBodyRegister);
            _sut.Setup(x => x.GetLayoutTemplate()).Returns(TestLayoutRegister);
        }

        [Fact]
        public void WhenSpecifiedRegisterEmailType_ThenCorrectLayoutIsTaken_AndCorrectBodyIsInsertedIntoLayout()
        {
            //Act
            var emailBody = _sut.Object.GetEmailBody(EmailType.Register, _customParameters);

            //Then
            emailBody.Should().Be.EqualTo(FullEmailRegister);
            emailBody.Should().Not.Contain("{{BodyHtml}}");
            emailBody.Should().Not.Contain("{{UserName}}");
        }

        [Fact]
        public void WhenSpecifiedChangePasswordEmailType_ThenCorrectLayoutIsTaken_AndCorrectBodyIsInsertedIntoLayout()
        {
            //Act
            var emailBody = _sut.Object.GetEmailBody(EmailType.ChangePassword, _customParameters);

            //Then
            emailBody.Should().Be.EqualTo(FullEmailChangeReset);
            emailBody.Should().Not.Contain("{{BodyHtml}}");
            emailBody.Should().Not.Contain("{{TekstLayoutPlz}}");
        }

        [Fact]
        public void WhenSpecifiedResetPasswordEmailType_ThenCorrectLayoutIsTaken_AndCorrectBodyIsInsertedIntoLayout()
        {
            //Act
            var emailBody = _sut.Object.GetEmailBody(EmailType.ResetPassword, _customParameters);

            //Then
            emailBody.Should().Be.EqualTo(FullEmailChangeReset);
            emailBody.Should().Not.Contain("{{BodyHtml}}");
            emailBody.Should().Not.Contain("{{TekstLayoutPlz}}");
        }
    }
}
