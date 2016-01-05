using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.Mvc;
using ParkingATHWeb.Areas.Admin.Controllers.Base;
using ParkingATHWeb.Areas.Admin.ViewModels.User;
using ParkingATHWeb.Business.Services;
using ParkingATHWeb.Contracts.DTO.User;

namespace ParkingATHWeb.Areas.Admin.Controllers
{
    public class AdminUserController : AdminServiceController<AdminUserListItemViewModel, AdminUserCreateViewModel, AdminUserEditViewModel, AdminUserDeleteViewModel, UserBaseDto, int>
    {
        private readonly UserService _entityService;

        public AdminUserController(UserService entityService) : base(entityService)
        {
            _entityService = entityService;
        }

        public override async Task<IActionResult> Create(AdminUserCreateViewModel model)
        {
            var serviceResult = await _entityService.CreateAsync(Mapper.Map<UserBaseDto>(model), model.Password);
            return ReturnWithModelBase(model, serviceResult);
        }
    }
}
