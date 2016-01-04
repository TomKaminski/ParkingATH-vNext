using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.Mvc;
using ParkingATHWeb.Areas.Admin.ViewModels.AdminUser;
using ParkingATHWeb.Contracts.Services;

namespace ParkingATHWeb.Areas.Admin.Controllers
{
    public class AdminUserController : AdminBaseController
    {
        private readonly IUserService _userService;

        public AdminUserController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            return View((await _userService.GetAllForAdminAsync()).Result.Select(Mapper.Map<AdminUserListItemViewModel>));
        }
    }
}
