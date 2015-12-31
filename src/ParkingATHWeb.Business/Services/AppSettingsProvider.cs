using System;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.PlatformAbstractions;
using ParkingATHWeb.Contracts.DTO;
using ParkingATHWeb.Contracts.Services;
using ParkingATHWeb.Shared.Enums;

namespace ParkingATHWeb.Business.Services
{
    public class AppSettingsProvider : IAppSettingsProvider
    {
        private readonly IApplicationEnvironment _appEnv;
        private const string DefaultSmtpConfigurationKey = "Settings:SmtpConfiguration:";

        public AppSettingsProvider(IApplicationEnvironment appEnv)
        {
            _appEnv = appEnv;
        }

        public IConfigurationRoot GetAppSettings(params AppSettingsType[] settings)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(_appEnv.ApplicationBasePath);

            foreach (var appSettingsType in settings)
            {
                builder.AddJsonFile($"{appSettingsType}.json");
            }

            return builder.Build();
        }

        public SmtpSettings GetSmtpSettings()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(_appEnv.ApplicationBasePath)
                .AddJsonFile($"{AppSettingsType.DefaultSettings}.json")
                .Build();

            return new SmtpSettings
            {
                SmtpDeliveryMethod = SmtpDeliveryMethod.Network,
                DeliveryFormat = SmtpDeliveryFormat.International,
                Host = configuration[$"{DefaultSmtpConfigurationKey}host"],
                Port = Convert.ToInt32(configuration[$"{DefaultSmtpConfigurationKey}port"]),
                UseDefaultCredentials =
                    Convert.ToBoolean(configuration[$"{DefaultSmtpConfigurationKey}defaultCredentials"]),
                EnableSsl = Convert.ToBoolean(configuration[$"{DefaultSmtpConfigurationKey}enableSsl"]),
                From = configuration[$"{DefaultSmtpConfigurationKey}from"],
                Credentials =
                    new NetworkCredential(configuration[$"{DefaultSmtpConfigurationKey}userName"],
                        configuration[$"{DefaultSmtpConfigurationKey}password"])
            };
        }
    }
}
