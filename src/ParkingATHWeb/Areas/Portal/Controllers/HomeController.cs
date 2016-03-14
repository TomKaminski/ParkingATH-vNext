using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using ParkingATHWeb.Areas.Portal.Controllers.Base;
using ParkingATHWeb.Areas.Portal.ViewModels.Chart;
using ParkingATHWeb.Areas.Portal.ViewModels.Weather;
using ParkingATHWeb.Contracts.DTO.Chart;
using ParkingATHWeb.Contracts.Services;
using ParkingATHWeb.Infrastructure.Attributes;
using ParkingATHWeb.Shared.Enums;
using ParkingATHWeb.Shared.Helpers;

namespace ParkingATHWeb.Areas.Portal.Controllers
{
    [Area("Portal")]
    [Route("[area]")]
    [Authorize]
    public class HomeController : BaseController
    {
        private readonly IWeatherService _weatherService;
        private readonly IUserService _userService;
        private readonly IGateUsageService _gateUsageService;
        private readonly IMapper _mapper;
        private readonly IChartService _chartService;
        private readonly IPortalMessageService _portalMessageService;

        public HomeController(IWeatherService weatherService, IUserService userService, IGateUsageService gateUsageService, IMapper mapper, IChartService chartService, IPortalMessageService portalMessageService)
        {
            _weatherService = weatherService;
            _userService = userService;
            _gateUsageService = gateUsageService;
            _mapper = mapper;
            _chartService = chartService;
            _portalMessageService = portalMessageService;
        }

        [Route("~/[area]")]
        public async Task<IActionResult> Index()
        {
            return View((await _portalMessageService.GetUnreadClustersCountAsync(CurrentUser.UserId.Value)).Result);
        }

        [Route("~/[area]/Dashboard")]
        public IActionResult Dashboard()
        {
            return PartialView();
        }

        [Route("[controller]/[action]")]
        [ValidateAntiForgeryTokenFromHeader]
        public async Task<IActionResult> GetWeatherData()
        {
            var weatherData = _mapper.Map<WeatherDataViewModel>((await _weatherService.GetLatestWeatherDataAsync()).Result);
            return Json(weatherData);
        }

        [Route("[controller]/[action]")]
        [ValidateAntiForgeryTokenFromHeader]
        public async Task<IActionResult> GetUserChargesData()
        {
            var user = (await _userService.GetByEmailAsync(CurrentUser.Email)).Result;
            var userId = user.Id;
            var endDate = DateTime.Today.AddDays(1).AddSeconds(-1);
            var startDate = DateTime.Today.AddDays(-6);
            var userGateUsages = (await _gateUsageService.GetAllAsync(x => x.UserId == userId)).Result.ToList();

            var lineChartData = await _chartService.GetDataAsync(new ChartRequestDto(startDate, endDate, ChartType.GateOpenings, ChartGranuality.PerDay, userId));

            return Json(new
            {
                chargesUsed = userGateUsages.Count,
                chargesLeft = user.Charges,
                lineChartData = _mapper.Map<ChartDataReturnModel>(lineChartData.Result)
            });
        }
    }
}
