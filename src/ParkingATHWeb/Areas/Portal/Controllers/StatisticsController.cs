using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using ParkingATHWeb.ApiModels.Base;
using ParkingATHWeb.Areas.Admin.ViewModels.Order;
using ParkingATHWeb.Areas.Portal.Controllers.Base;
using ParkingATHWeb.Areas.Portal.ViewModels.Chart;
using ParkingATHWeb.Areas.Portal.ViewModels.GateUsage;
using ParkingATHWeb.Areas.Portal.ViewModels.User;
using ParkingATHWeb.Contracts.DTO.Chart;
using ParkingATHWeb.Contracts.Services;
using ParkingATHWeb.Infrastructure.Attributes;
using ParkingATHWeb.Shared.Enums;
using ParkingATHWeb.ViewModels.Base;

namespace ParkingATHWeb.Areas.Portal.Controllers
{
    [Area("Portal")]
    [Route("[area]/[controller]")]
    [Authorize]
    public class StatisticsController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IChartService _chartService;
        private readonly IOrderService _orderService;
        private readonly IGateUsageService _gateUsageService;

        public StatisticsController(IChartService chartService, IMapper mapper, IGateUsageService gateUsageService, IOrderService orderService)
        {
            _chartService = chartService;
            _mapper = mapper;
            _gateUsageService = gateUsageService;
            _orderService = orderService;
        }

        [Route("")]
        public IActionResult Index()
        {
            return PartialView();
        }

        [Route("GetChartData")]
        [ValidateAntiForgeryTokenFromHeader]
        [HttpPost]
        public async Task<IActionResult> GetChartData([FromBody]ChartDataRequest model)
        {
            if (ModelState.IsValid)
            {
                var serviceRequest = _mapper.Map<ChartRequestDto>(model);
                serviceRequest.UserId = CurrentUser.UserId.Value;
                var chartDataResult = await _chartService.GetDataAsync(serviceRequest);
                if (chartDataResult.IsValid)
                {
                    return Json(SmartJsonResult<ChartDataReturnModel>
                        .Success(_mapper.Map<ChartDataReturnModel>(chartDataResult.Result)));
                }

                return Json(SmartJsonResult.Failure(chartDataResult.ValidationErrors));
            }
            return Json(SmartJsonResult.Failure(GetModelStateErrors(ModelState)));
        }

        [Route("GetDefaultChartData")]
        [ValidateAntiForgeryTokenFromHeader]
        [HttpPost]
        public async Task<IActionResult> GetDefaultChartData()
        {
            if (ModelState.IsValid)
            {
                var chartDataAndPreferencesResult = await _chartService.GetDefaultDataAsync(CurrentUser.UserId.Value);
                if (chartDataAndPreferencesResult.IsValid)
                {
                    var result = new
                    {
                        gateUsagesData = _mapper.Map<ChartDataReturnModel>(chartDataAndPreferencesResult.Result.FirstOrDefault(x => x.Key == ChartType.GateOpenings).Value),
                        ordersData = _mapper.Map<ChartDataReturnModel>(chartDataAndPreferencesResult.Result.FirstOrDefault(x => x.Key == ChartType.Orders).Value),
                    };

                    return Json(SmartJsonResult<object, ChartPreferencesReturnModel>
                        .Success(result, _mapper.Map<ChartPreferencesReturnModel>(chartDataAndPreferencesResult.SecondResult)));
                }
                return Json(SmartJsonResult.Failure(chartDataAndPreferencesResult.ValidationErrors));
            }
            return Json(SmartJsonResult.Failure(GetModelStateErrors(ModelState)));
        }

        [HttpPost]
        [ValidateAntiForgeryTokenFromHeader]
        [Route("OrderDateRangeList")]
        public async Task<IActionResult> OrderDateRangeList([FromBody]SmartParkListDateRangeRequestViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model = new SmartParkListDateRangeRequestViewModel
                {
                    DateFrom = DateTime.Today.AddDays(-6),
                    DateTo = DateTime.Now
                };
            }

            var dateFrom = model.DateFrom;
            var dateTo = model.DateTo;
            var userId = CurrentUser.UserId.Value;

            var serviceResult = await _orderService.GetAllAsync(x => x.Date >= dateFrom && x.Date <= dateTo && x.UserId == userId);
            return Json(serviceResult.IsValid
                ? SmartJsonResult<SmartParkListWithDateRangeViewModel<ShopOrderItemViewModel>>
                .Success(new SmartParkListWithDateRangeViewModel<ShopOrderItemViewModel>
                {
                    ListItems = serviceResult.Result.OrderByDescending(x=>x.Date).Select(_mapper.Map<ShopOrderItemViewModel>),
                    DateTo = model.DateTo,
                    DateFrom = model.DateFrom
                })
                : SmartJsonResult<SmartParkListWithDateRangeViewModel<ShopOrderItemViewModel>>.Failure(serviceResult.ValidationErrors));
        }

        [HttpPost]
        [ValidateAntiForgeryTokenFromHeader]
        [Route("GtDateRangeList")]
        public async Task<IActionResult> GtDateRangeList([FromBody]SmartParkListDateRangeRequestViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model = new SmartParkListDateRangeRequestViewModel
                {
                    DateFrom = DateTime.Today.AddDays(-6),
                    DateTo = DateTime.Now
                };
            }

            var dateFrom = model.DateFrom;
            var dateTo = model.DateTo;
            var userId = CurrentUser.UserId.Value;

            var serviceResult = await _gateUsageService.GetAllAsync(x => x.DateOfUse >= dateFrom && x.DateOfUse <= dateTo && x.UserId == userId);
            return Json(serviceResult.IsValid
                ? SmartJsonResult<SmartParkListWithDateRangeViewModel<GateOpeningViewModel>>
                .Success(new SmartParkListWithDateRangeViewModel<GateOpeningViewModel>
                {
                    ListItems = serviceResult.Result.OrderByDescending(x=>x.DateOfUse).Select(_mapper.Map<GateOpeningViewModel>),
                    DateTo = model.DateTo,
                    DateFrom = model.DateFrom
                })
                : SmartJsonResult<SmartParkListWithDateRangeViewModel<GateOpeningViewModel>>.Failure(serviceResult.ValidationErrors));
        }
    }
}
