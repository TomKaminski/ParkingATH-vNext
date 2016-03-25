using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using ParkingATHWeb.Areas.Portal.Controllers.Base;

namespace ParkingATHWeb.Areas.Admin.Controllers.Base
{
    [Area("Admin")]
    [Authorize(Policy = "Admin")]
    [Route("[area]/[controller]/[action]")]
    public class AdminBaseController:BaseController
    {
    }
}
