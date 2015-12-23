using Microsoft.AspNet.Mvc;

namespace ParkingATHWeb.Areas.Portal.Controllers
{
    [Area("Portal")]
    //[Route("[area]/[controller]")]
    public class AccountController : Controller
    {
        //[Route("[action]")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
