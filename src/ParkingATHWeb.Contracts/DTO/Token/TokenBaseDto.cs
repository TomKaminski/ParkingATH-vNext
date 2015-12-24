using System;
using ParkingATHWeb.Contracts.Common;
using ParkingATHWeb.Shared.Enums;
using ParkingATHWeb.Shared.Helpers;

namespace ParkingATHWeb.Contracts.DTO.Token
{
    public class TokenBaseDto : BaseDto<long>
    {
        public DateTime ValidTo { get; set; }
        public TokenType TokenType { get; set; }
        public Guid SecureToken { get; set; }

        public string BuildEncryptedToken(string email)
        {
            return EncryptHelper.Encrypt($"{email}&{(int)TokenType}&{SecureToken}");
        }

        public bool IsExpired()
        {
            return ValidTo > DateTime.Now;
        }
    }
}
