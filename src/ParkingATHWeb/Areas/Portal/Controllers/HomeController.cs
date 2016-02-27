using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using ParkingATHWeb.Areas.Portal.Controllers.Base;
using ParkingATHWeb.Areas.Portal.ViewModels.Weather;
using ParkingATHWeb.Contracts.Services;
using ParkingATHWeb.Infrastructure.Attributes;

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
        public HomeController(IWeatherService weatherService, IUserService userService, IGateUsageService gateUsageService, IMapper mapper)
        {
            _weatherService = weatherService;
            _userService = userService;
            _gateUsageService = gateUsageService;
            _mapper = mapper;
        }

        [Route("~/[area]")]
        public IActionResult Index()
        {
            return View();
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
            var lineChartData = _gateUsageService.GetGateUsagesChartData(userGateUsages.Where(x => x.DateOfUse >= startDate), startDate, endDate).Result;

            return Json(new
            {
                chargesUsed = userGateUsages.Count,
                chargesLeft = user.Charges,
                lineChartData = new
                {
                    labels = lineChartData.Select(x => $"{x.Key.Day}.{x.Key.Month}").ToArray(),
                    data = lineChartData.Select(x => x.Value).ToArray()
                }
            });
        }

        [Route("[controller]/[action]")]
        [Obsolete("Use partial methods instead")]
        public async Task<IActionResult> DashboardData()
        {
            var user = (await _userService.GetByEmailAsync(CurrentUser.Email)).Result;
            var userId = user.Id;

            var weatherData = _mapper.Map<WeatherDataViewModel>((await _weatherService.GetLatestWeatherDataAsync()).Result);
            var userGateUsages = (await _gateUsageService.GetAllAsync(x => x.UserId == userId)).Result.ToList();

            var endDate = DateTime.Today.AddDays(1).AddSeconds(-1);
            var startDate = DateTime.Today.AddDays(-6);
            var lineChartData = _gateUsageService.GetGateUsagesChartData(userGateUsages.Where(x => x.DateOfUse >= startDate), startDate, endDate).Result;
            return Json(new
            {
                chargesUsed = userGateUsages.Count,
                chargesLeft = user.Charges,
                lineChartData = new
                {
                    labels = lineChartData.Select(x => $"{x.Key.Day}.{x.Key.Month}").ToArray(),
                    data = lineChartData.Select(x => x.Value).ToArray()
                },
                weatherData
            });
        }
    }
}
