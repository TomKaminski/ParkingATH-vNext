using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using ParkingATHWeb.ApiModels.Base;
using ParkingATHWeb.ApiModels.Panel;
using ParkingATHWeb.Areas.Portal.ViewModels.Payment;
using ParkingATHWeb.Contracts.DTO.Payments;
using ParkingATHWeb.Contracts.Services.Payments;
using ParkingATHWeb.Shared.Enums;

namespace ParkingATHWeb.Controllers
{
    [Route("api/Payment")]
    [AllowAnonymous]
    public class PaymentApiController : BaseApiController
    {
        private readonly IPayuService _payuService;
        private readonly IMapper _mapper;


        public PaymentApiController(IPayuService payuService, IMapper mapper)
        {
            _payuService = payuService;
            _mapper = mapper;
        }


        [HttpPost]
        [Route("ProcessPayment")]
        public async Task<SmartJsonResult<PaymentResponseViewModel>> ProcessPayment([FromBody] PaymentRequestApiModel model)
        {
            if (ModelState.IsValid)
            {
                model.CustomerIP = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "127.0.0.1";

                var request = _mapper.Map<PaymentRequest>(model);
                request.notifyUrl = Url.Action("Notify", "Payment", new { area = "Portal" }, "http");
                request.continueUrl = "http://localhost:3000/#/app/thanks";
                var payuServiceResult = await _payuService.ProcessPaymentAsync(request, model.UserId, OrderPlace.Panel);

                if (payuServiceResult.IsValid)
                {
                    return
                            SmartJsonResult<PaymentResponseViewModel>.Success(
                                _mapper.Map<PaymentResponseViewModel>(payuServiceResult.Result));
                }
                return SmartJsonResult<PaymentResponseViewModel>.Failure(payuServiceResult.ValidationErrors);
            }
            return SmartJsonResult<PaymentResponseViewModel>.Failure(GetModelStateErrors(ModelState));

        }


        [HttpPost]
        [Route("ProcessCardPayment")]
        public async Task<SmartJsonResult<PaymentCardResponse>> ProcessCardPayment([FromBody] PaymentRequestApiModel model)
        {
            if (ModelState.IsValid)
            {
                model.CustomerIP = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "127.0.0.1";

                var request = _mapper.Map<PaymentCardRequest>(model);
                request.notifyUrl = Url.Action("Notify", "Payment", new { area = "Portal" }, "http");
                var payuServiceResult = await _payuService.ProcessCardPaymentAsync(request, model.UserId, OrderPlace.Panel);

                if (payuServiceResult.IsValid)
                {
                    return
                            SmartJsonResult<PaymentCardResponse>.Success(payuServiceResult.Result);
                }
                return SmartJsonResult<PaymentCardResponse>.Failure(payuServiceResult.ValidationErrors);
            }
            return SmartJsonResult<PaymentCardResponse>.Failure(GetModelStateErrors(ModelState));

        }
    }
}
