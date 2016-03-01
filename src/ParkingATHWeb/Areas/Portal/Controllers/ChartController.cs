using AutoMapper;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using ParkingATHWeb.Areas.Portal.Controllers.Base;
using ParkingATHWeb.Contracts.Services;

namespace ParkingATHWeb.Areas.Portal.Controllers
{
    [Area("Portal")]
    [Route("[area]")]
    [Authorize]
    public class ChartController : BaseController
    {
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;
        private readonly IGateUsageService _gateUsageService;
        private readonly IMapper _mapper;

        public ChartController(IOrderService orderService, IUserService userService, IGateUsageService gateUsageService, IMapper mapper)
        {
            _orderService = orderService;
            _userService = userService;
            _gateUsageService = gateUsageService;
            _mapper = mapper;
        }


        [Route("~/[area]")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
