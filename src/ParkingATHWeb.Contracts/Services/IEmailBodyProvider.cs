using System.Collections.Generic;
using ParkingATHWeb.Contracts.Services.Base;
using ParkingATHWeb.Shared.Enums;

namespace ParkingATHWeb.Contracts.Services
{
    public interface IEmailBodyProvider : IDependencyService
    {
        string GetEmailBody(EmailType type, Dictionary<string, string> parameters);
    }
}