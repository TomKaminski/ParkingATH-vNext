using System.Collections.Generic;
using ParkingATHWeb.Contracts.DTO.User;
using ParkingATHWeb.Contracts.Services.Base;
using ParkingATHWeb.Shared.Enums;

namespace ParkingATHWeb.Contracts.Services
{
    public interface IEmailBodyProvider : IDependencyService
    {
        string GetEmailBody(EmailType type, UserBaseDto userData, Dictionary<string, string> parameters);
    }
}