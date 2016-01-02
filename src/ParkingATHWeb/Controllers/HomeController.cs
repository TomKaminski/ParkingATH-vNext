using Microsoft.AspNet.Mvc;

namespace ParkingATHWeb.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
