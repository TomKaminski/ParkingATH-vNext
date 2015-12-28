using System;
using ParkingATHWeb.Contracts.Common;
using ParkingATHWeb.Shared.Enums;
using ParkingATHWeb.Shared.Helpers;
using Newtonsoft.Json;

namespace ParkingATHWeb.Contracts.DTO.Token
{
    public class TokenBaseDto : BaseDto<long>
    {
        public DateTime? ValidTo { get; set; }
        public TokenType TokenType { get; set; }
        public Guid SecureToken { get; set; }

        public string BuildEncryptedToken()
        {
            return EncryptHelper.Encrypt(JsonConvert.SerializeObject(this));
        }

        public bool NotExpired()
        {
            return ValidTo == null || ValidTo > DateTime.Now;
        }
    }
}
