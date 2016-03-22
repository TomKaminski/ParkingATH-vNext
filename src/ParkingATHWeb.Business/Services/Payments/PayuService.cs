using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ParkingATHWeb.Contracts.Common;
using ParkingATHWeb.Contracts.DTO.Order;
using ParkingATHWeb.Contracts.DTO.Payments;
using ParkingATHWeb.Contracts.Services;
using ParkingATHWeb.Contracts.Services.Payments;
using ParkingATHWeb.DataAccess.Interfaces;
using ParkingATHWeb.Shared.Enums;

namespace ParkingATHWeb.Business.Services.Payments
{
    public class PayuService : IPayuService
    {
        private const string HostAddress = "https://secure.payu.com/api/v2_1/orders";

        private readonly IPaymentAuthorizeService _paymentAuthorizeService;
        private readonly IPriceTresholdRepository _pricesRepository;
        private readonly IOrderService _orderService;
        private readonly PaymentSettings _paymentSettings;


        public PayuService(IPaymentAuthorizeService paymentAuthorizeService, IPriceTresholdRepository pricesRepository,
            IOrderService orderService, IAppSettingsProvider appSettingsProvider)
        {
            _paymentAuthorizeService = paymentAuthorizeService;
            _pricesRepository = pricesRepository;
            _orderService = orderService;
            _paymentSettings = appSettingsProvider.GetPaymentSettings();
        }

        public async Task<ServiceResult<PaymentResponse>> ProcessPaymentAsync(PaymentRequest request, int userId, OrderPlace orderPlace)
        {
            var authServiceResult = await _paymentAuthorizeService.GetAuthorizeTokenAsync();
            if (authServiceResult.IsValid)
            {
                using (var client = new HttpClient(new HttpClientHandler
                {
                    AllowAutoRedirect = false
                }))
                {
                    client.BaseAddress = new Uri(_paymentSettings.HostAddress);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                        authServiceResult.Result.access_token);

                    var orderPaymentInfo = await PrepareCompletePayuRequestAsync(request);
                    var requestBody = JsonConvert.SerializeObject(request);

                    var response = await client.PostAsync(_paymentSettings.OrderCreateEndpoint,
                                new StringContent(requestBody, Encoding.UTF8, "application/json"));

                    if (response.IsSuccessStatusCode || response.StatusCode == HttpStatusCode.Found)
                    {
                        var responseObj = JsonConvert.DeserializeObject<PaymentResponse>(await response.Content.ReadAsStringAsync());
                        await CreateNewOrderAsync(request, userId, orderPlace, orderPaymentInfo);
                        if (responseObj.status.statusCode == "SUCCESS")
                        {
                            return ServiceResult<PaymentResponse>.Success(responseObj);
                        }
                    }
                    return ServiceResult<PaymentResponse>.Failure(response.ReasonPhrase);
                }
            }
            return ServiceResult<PaymentResponse>.Failure(authServiceResult.ValidationErrors);
        }

        private async Task<OrderPaymentInfo> PrepareCompletePayuRequestAsync(PaymentRequest request)
        {
            var product = request.products.First();

            var priceTreshold =
                (await _pricesRepository.GetAllAsync(x => x.MinCharges <= Convert.ToInt32(product.quantity) && !x.IsDeleted))
                    .OrderByDescending(x => x.MinCharges).First();

            var totalPrice = (priceTreshold.PricePerCharge * Convert.ToInt32(product.quantity));

            product.unitPrice = (priceTreshold.PricePerCharge * 100).ToString("####");
            request.extOrderId = (await _orderService.GenerateExternalOrderIdAsync()).Result.ToString();
            request.totalAmount = (totalPrice * 100).ToString("####");
            request.merchantPosId = _paymentSettings.PosID;

            return new OrderPaymentInfo
            {
                TotalAmount = totalPrice,
                PricePerCharge = priceTreshold.PricePerCharge,
                PriceTresholdId = priceTreshold.Id
            };
        }

        private async Task CreateNewOrderAsync(PaymentRequest request, int userId, OrderPlace orderPlace, OrderPaymentInfo paymentInfo)
        {
            var order = new OrderBaseDto
            {
                UserId = userId,
                Date = DateTime.Now,
                ExtOrderId = new Guid(request.extOrderId),
                NumOfCharges = Convert.ToInt32(request.products[0].quantity),
                OrderPlace = orderPlace,
                OrderState = OrderStatus.Pending,
                PricePerCharge = paymentInfo.PricePerCharge,
                PriceTresholdId = paymentInfo.PriceTresholdId,
                Price = paymentInfo.TotalAmount,
            };

            await _orderService.CreateAsync(order);
        }
    }
}
