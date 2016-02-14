using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using ParkingATHWeb.Areas.Portal.Controllers.Base;

namespace ParkingATHWeb.Areas.Portal.Controllers
{
    [Area("Portal")]
    [Route("[area]/Statystyki")]
    [Authorize]
    public class StatisticsController : BaseController
    {
        [Route("")]
        public IActionResult Index()
        {
            return PartialView();
        }
    }
}
