using System;
using ParkingATHWeb.Shared.Helpers;
using SharpTestsEx;
using Xunit;

namespace ParkingATHWeb.Shared.Tests
{
    public class PasswordHasherTests
    {
        private readonly PasswordHasher _sut;
        private const string ValidPassword = "Password123";
        private const string NotValidPassword = "Password12345678";
        private const char SplitCharacter = ':';

        public PasswordHasherTests()
        {
            _sut = new PasswordHasher();
        }

        [Fact]
        public void WhenEncryptPassword_ReturnHashAndSalt()
        {
            //Act
            var encryptedString = _sut.CreateHash(ValidPassword);
            var splitted = encryptedString.Split(new[] { SplitCharacter }, StringSplitOptions.RemoveEmptyEntries);

            //Then
            splitted.Length.Should().Be.EqualTo(2);
            splitted[0].Should().Not.Be.Empty();
            splitted[1].Should().Not.Be.Empty();
        }

        [Fact]
        public void WhenEncryptPassword_ThenValidateIt_ResultIsValid()
        {
            //Act
            var encryptedString = _sut.CreateHash(ValidPassword);
            var splitted = encryptedString.Split(new[] { SplitCharacter }, StringSplitOptions.RemoveEmptyEntries);
            var validationResult = _sut.ValidatePassword(ValidPassword, splitted[1], splitted[0]);

            //Then
            splitted.Length.Should().Be.EqualTo(2);
            splitted[0].Should().Not.Be.Empty();
            splitted[1].Should().Not.Be.Empty();
            validationResult.Should().Be.True();
        }

        [Fact]
        public void WhenEncryptPassword_ThenValidateIt_ResultIsNotValid()
        {
            //Act
            var encryptedString = _sut.CreateHash(ValidPassword);
            var splitted = encryptedString.Split(new[] { SplitCharacter }, StringSplitOptions.RemoveEmptyEntries);
            var validationResult = _sut.ValidatePassword(NotValidPassword, splitted[1], splitted[0]);

            //Then
            splitted.Length.Should().Be.EqualTo(2);
            splitted[0].Should().Not.Be.Empty();
            splitted[1].Should().Not.Be.Empty();
            validationResult.Should().Be.False();
        }
    }
}
