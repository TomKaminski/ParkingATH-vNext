using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using ParkingATHWeb.ApiModels.Base;
using ParkingATHWeb.Areas.Portal.Controllers.Base;
using ParkingATHWeb.Areas.Portal.ViewModels.Chart;
using ParkingATHWeb.Contracts.DTO.Chart;
using ParkingATHWeb.Contracts.Services;

namespace ParkingATHWeb.Areas.Portal.Controllers
{
    [Area("Portal")]
    [Route("[area]/Statystyki")]
    [Authorize]
    public class StatisticsController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IChartService _chartService;

        public StatisticsController(IChartService chartService, IMapper mapper)
        {
            _chartService = chartService;
            _mapper = mapper;
        }

        [Route("")]
        public IActionResult Index()
        {
            return PartialView();
        }

        [Route("GetChartData")]
        public async Task<IActionResult> GetChartData([FromBody]ChartDataRequest model)
        {
            if (ModelState.IsValid)
            {
                var serviceRequest = _mapper.Map<ChartRequestDto>(model);
                serviceRequest.UserId = CurrentUser.UserId.Value;
                var chartDataResult = await _chartService.GetDataAsync(serviceRequest);
                if (chartDataResult.IsValid)
                {
                    return Json(SmartJsonResult<ChartDataReturnModel>.Success(_mapper.Map<ChartDataReturnModel>(chartDataResult.Result)));
                }
                return Json(SmartJsonResult.Failure(chartDataResult.ValidationErrors));
            }
            return Json(SmartJsonResult.Failure(GetModelStateErrors(ModelState)));
        }
    }
}
