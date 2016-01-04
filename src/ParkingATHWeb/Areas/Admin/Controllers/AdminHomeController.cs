using Microsoft.AspNet.Mvc;

namespace ParkingATHWeb.Areas.Admin.Controllers
{
    public class AdminHomeController : AdminBaseController
    {
        [Route("~/[area]")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
