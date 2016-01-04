using Microsoft.AspNet.Mvc;

namespace ParkingATHWeb.Controllers
{

    public class WelcomeController : Controller
    {
        [Route("~/")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
