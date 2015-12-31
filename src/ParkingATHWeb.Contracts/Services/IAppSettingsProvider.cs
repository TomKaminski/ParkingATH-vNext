using Microsoft.Extensions.Configuration;
using ParkingATHWeb.Contracts.DTO;
using ParkingATHWeb.Contracts.Services.Base;
using ParkingATHWeb.Shared.Enums;

namespace ParkingATHWeb.Contracts.Services
{
    public interface IAppSettingsProvider : IDependencyService
    {
        IConfigurationRoot GetAppSettings(params AppSettingsType[] settings);
        SmtpSettings GetSmtpSettings();
    }
}
