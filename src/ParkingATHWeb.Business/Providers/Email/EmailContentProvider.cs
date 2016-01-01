using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.PlatformAbstractions;
using ParkingATHWeb.Contracts.Services;
using ParkingATHWeb.Shared.Enums;

namespace ParkingATHWeb.Business.Providers.Email
{
    public class EmailContentProvider : IEmailContentProvider
    {
        private readonly IApplicationEnvironment _appEnv;
        private readonly IAppSettingsProvider _appSettingsProvider;

        private const string EmailsBasePath = "/Content/Emails";
        private const string BodyMarker = "{{BodyHtml}}";

        public EmailContentProvider()
        {
            
        }

        public EmailContentProvider(IApplicationEnvironment appEnv, IAppSettingsProvider appSettingsProvider)
        {
            _appEnv = appEnv;
            _appSettingsProvider = appSettingsProvider;
        }

        public virtual string GetEmailBody(EmailType type, Dictionary<string,string> parameters)
        {
            var validTemplate = GetValidTemplateString(type);
            var templateWithLayout = InsertBodyIntoLayout(validTemplate);
            return PrepareEmailBody(templateWithLayout, parameters);
        }

        private string PrepareEmailBody(string template, Dictionary<string, string> parameters)
        {
            var localTemplate = template;
            foreach (var parameter in parameters)
            {
                localTemplate = localTemplate.Replace("{{" + parameter.Key + "}}", parameter.Value);
            }
            return localTemplate;
        }

        public virtual string GetLayoutTemplate()
        {
            return File.ReadAllText(GetLayoutPath());
        }

        private string GetLayoutPath()
        {
            return $"{_appEnv.ApplicationBasePath}{EmailsBasePath}/_EmailLayout.html";
        }

        public virtual string GetValidTemplateString(EmailType type)
        {
            switch (type)
            {
                case EmailType.Register:
                    return File.ReadAllText($"{_appEnv.ApplicationBasePath}{EmailsBasePath}/Register.html");
                case EmailType.ResetPassword:
                    return File.ReadAllText($"{_appEnv.ApplicationBasePath}{EmailsBasePath}/ResetPassword.html");
            }
            return "";
        }

        private string InsertBodyIntoLayout(string bodyHtml)
        {
            return GetLayoutTemplate().Replace(BodyMarker, bodyHtml);
        }

        public virtual string GetEmailTitle(EmailType type)
        {
            var conf = _appSettingsProvider.GetAppSettings(AppSettingsType.Resources);
            switch (type)
            {
                case EmailType.Register:
                    return conf["EmailResources:RegisterEmail_Title"];
                case EmailType.ResetPassword:
                    return conf["EmailResources:ResetPassword_Title"];
            }
            throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }
}
