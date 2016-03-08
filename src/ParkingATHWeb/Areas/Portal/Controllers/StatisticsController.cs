using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using ParkingATHWeb.ApiModels.Base;
using ParkingATHWeb.Areas.Portal.Controllers.Base;
using ParkingATHWeb.Areas.Portal.ViewModels.Chart;
using ParkingATHWeb.Contracts.DTO.Chart;
using ParkingATHWeb.Contracts.DTO.UserPreferences;
using ParkingATHWeb.Contracts.Services;
using ParkingATHWeb.Infrastructure.Attributes;
using ParkingATHWeb.Shared.Enums;

namespace ParkingATHWeb.Areas.Portal.Controllers
{
    [Area("Portal")]
    [Route("[area]/Statystyki")]
    [Authorize]
    public class StatisticsController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IChartService _chartService;
        private readonly IUserPreferencesService _userPreferencesService;

        public StatisticsController(IChartService chartService, IMapper mapper, IUserPreferencesService userPreferencesService)
        {
            _chartService = chartService;
            _mapper = mapper;
            _userPreferencesService = userPreferencesService;
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
    }
}
