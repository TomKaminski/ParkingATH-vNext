using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.PlatformAbstractions;
using ParkingATHWeb.Contracts.Services;
using ParkingATHWeb.Shared.Enums;

namespace ParkingATHWeb.Business.Providers.Email
{
    public class EmailBodyProvider : IEmailBodyProvider
    {
        private readonly IApplicationEnvironment _appEnv;
        private const string EmailsBasePath = "/Content/Emails";
        private const string BodyMarker = "{{BodyHtml}}";

        public EmailBodyProvider(IApplicationEnvironment appEnv)
        {
            _appEnv = appEnv;
        }

        public string GetEmailBody(EmailType type, Dictionary<string,string> parameters)
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

        private string GetLayoutTemplate()
        {
            return File.ReadAllText($"{_appEnv.ApplicationBasePath}{EmailsBasePath}/_EmailLayout.html");
        }

        private string GetValidTemplateString(EmailType type)
        {
            switch (type)
            {
                case EmailType.Register:
                    return File.ReadAllText($"{_appEnv.ApplicationBasePath}{EmailsBasePath}/Register.html");
                case EmailType.ResetPassword:
                    break;
                case EmailType.ChangePassword:
                    break;
            }
            return "";
        }

        private string InsertBodyIntoLayout(string bodyHtml)
        {
            return GetLayoutTemplate().Replace(BodyMarker, bodyHtml);
        }
    }
}
