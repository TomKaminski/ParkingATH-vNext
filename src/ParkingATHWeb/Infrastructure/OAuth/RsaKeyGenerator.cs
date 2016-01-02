using System.Security.Cryptography;

namespace ParkingATHWeb.Infrastructure.OAuth
{
    public class RsaKeyGenerator
    {
        private const string TokenAudience = "Myself";
        private const string TokenIssuer = "MyProject";

        private static string GenerateRsaKeys()
        {
            using (var rsa = new RSACryptoServiceProvider(1024))
            {
                try
                {
                    var publicKey = rsa.ExportParameters(true);
                    return rsa.ToXmlString(true);
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
        }
    }
}
