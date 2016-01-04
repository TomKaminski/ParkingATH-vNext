using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using ParkingATHWeb.Areas.Portal.Controllers.Base;

namespace ParkingATHWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "Admin")]
    [Route("[area]")]
    public class AdminBaseController:BaseController
    {
    }
}
