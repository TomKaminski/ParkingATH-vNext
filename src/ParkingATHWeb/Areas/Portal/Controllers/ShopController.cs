using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using ParkingATHWeb.Areas.Portal.Controllers.Base;

namespace ParkingATHWeb.Areas.Portal.Controllers
{
    [Area("Portal")]
    [Route("[area]/Sklep")]
    [Authorize]
    public class ShopController : BaseController
    {
        [Route("")]
        public IActionResult Index()
        {
            return PartialView();
        }
    }
}
