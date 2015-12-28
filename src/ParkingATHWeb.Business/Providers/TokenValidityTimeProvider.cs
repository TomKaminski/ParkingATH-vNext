using System;
using ParkingATHWeb.Shared.Enums;

namespace ParkingATHWeb.Business.Providers
{
    public static class TokenValidityTimeProvider
    {
        public static DateTime? GetValidToDate(TokenType tokenType)
        {
            switch (tokenType)
            {
                case TokenType.EmailChangeToken:
                    return DateTime.Now.AddDays(3);
                case TokenType.PasswordChangeResetToken:
                    return DateTime.Now.AddDays(3);
                case TokenType.ViewInBrowserToken:
                    return null;
                default:
                    return DateTime.Now;
            }
        }
    }
}
