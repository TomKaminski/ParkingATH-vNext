using System;
using ParkingATHWeb.Contracts.Common;

namespace ParkingATHWeb.Contracts.DTO.Token
{
    public class TokenBaseDto : BaseDto<long>
    {
        public string EncryptedToken { get; set; }
        public DateTime ValidTo { get; set; }
    }
}
