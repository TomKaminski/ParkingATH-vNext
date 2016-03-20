using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ParkingATHWeb.Contracts.Common;
using ParkingATHWeb.Contracts.DTO.Payments;
using ParkingATHWeb.Contracts.Services.Payments;

namespace ParkingATHWeb.Business.Services.Payments
{
    public class PaymentAuthorizeService : IPaymentAuthorizeService
    {
        private const string AuthorizeAddress = "https://secure.payu.com/pl/standard/user/oauth/authorize";
        private const string RequestContentType = "application/x-www-form-urlencoded";
        private const string POS_ID = "145227";
        private const string OAuthClientSecret = "12f071174cb7eb79d4aac5bc2f07563f";


        public async Task<ServiceResult<PaymentAuthorizationResponse>> GetAuthorizeTokenAsync()
        {
            using (var client = new HttpClient(new HttpClientHandler
            {
                AllowAutoRedirect = false
            }))
            {
                var postData = GetRequestBodyForAuthorization();

                HttpContent requestBody = new FormUrlEncodedContent(postData);

                var response = await client.PostAsync(AuthorizeAddress, requestBody);

                if (response.IsSuccessStatusCode)
                {
                    var stringResult = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<PaymentAuthorizationResponse>(stringResult);
                    return ServiceResult<PaymentAuthorizationResponse>.Success(result);
                }
                return ServiceResult<PaymentAuthorizationResponse>.Failure("Error");
            }
        }

        private static IEnumerable<KeyValuePair<string, string>> GetRequestBodyForAuthorization()
        {
            return new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("grant_type", "client_credentials"),
                    new KeyValuePair<string, string>("client_id", POS_ID),
                    new KeyValuePair<string, string>("client_secret", OAuthClientSecret)
                };
        }
    }
}
