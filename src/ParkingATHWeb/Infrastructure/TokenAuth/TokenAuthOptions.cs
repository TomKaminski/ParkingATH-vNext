using System;
using System.IdentityModel.Tokens;

namespace ParkingATHWeb.Infrastructure.TokenAuth
{
    [Obsolete("We are not using JW Tokens for now.")]
    public class TokenAuthOptions
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public SigningCredentials SigningCredentials { get; set; }
    }
}
