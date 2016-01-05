using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.Mvc;
using ParkingATHWeb.Areas.Admin.Controllers.Base;
using ParkingATHWeb.Areas.Admin.ViewModels.AdminUser;
using ParkingATHWeb.Contracts.Services;

namespace ParkingATHWeb.Areas.Admin.Controllers
{
    [Route("[area]/Uzytkownicy")]
    public class AdminUserController : AdminBaseController
    {
        private readonly IUserService _userService;

        public AdminUserController(IUserService userService)
        {
            _userService = userService;
        }

        [Route("")]
        public async Task<IActionResult> Index()
        {
            return View((await _userService.GetAllForAdminAsync()).Result.Select(Mapper.Map<AdminUserListItemViewModel>));
        }

        [HttpGet]
        [Route("Usun/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return View(Mapper.Map<AdminUserListItemViewModel>(await _userService.GetAdminAsync(id)));
        }

        [HttpPost]
        [Route("Usun/{id}")]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(AdminUserDeleteViewModel model)
        {
            var deleteServiceResult = await _userService.DeleteAsync(model.Id);
            return deleteServiceResult.IsValid 
                ? RedirectToAction("Index") 
                : ReturnModelWithError(model, deleteServiceResult);
        }
    }
}
