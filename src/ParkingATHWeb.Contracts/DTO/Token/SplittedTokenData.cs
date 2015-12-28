using System;
using ParkingATHWeb.Shared.Enums;

namespace ParkingATHWeb.Contracts.DTO.Token
{
    public class SplittedTokenData
    {
        public Guid SecureToken { get; set; }
        public TokenType TokenType { get; set; }
    }
}
