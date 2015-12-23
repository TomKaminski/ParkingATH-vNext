using Microsoft.AspNet.Mvc;

namespace ParkingATHWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Route("[area]/[controller]")]
    public class AdminController : Controller
    {
        //[Route("[action]")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
