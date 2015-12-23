using Microsoft.AspNet.Mvc;
using ParkingATHWeb.Contracts.Services;
using ParkingATHWeb.Shared.Helpers;

namespace ParkingATHWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IPriceTresholdService _priceTresholdService;

        public HomeController(IPasswordHasher passwordHasher, IPriceTresholdService priceTresholdService)
        {
            _passwordHasher = passwordHasher;
            _priceTresholdService = priceTresholdService;
        }

        public IActionResult Index()
        {
            var plz = _priceTresholdService.GetAll();
            var hashedTest = _passwordHasher.CreateHash("dasasdasdasd");
            return View((object)hashedTest);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
