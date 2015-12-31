using System.Collections.Generic;
using ParkingATHWeb.Contracts.Services.Base;
using ParkingATHWeb.Shared.Enums;

namespace ParkingATHWeb.Contracts.Services
{
    public interface IEmailContentProvider : IDependencyService
    {
        string GetEmailBody(EmailType type, Dictionary<string, string> parameters);
        string GetLayoutTemplate();
        string GetValidTemplateString(EmailType type);
        string GetEmailTitle(EmailType type);
    }
}