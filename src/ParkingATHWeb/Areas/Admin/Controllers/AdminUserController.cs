using ParkingATHWeb.Areas.Admin.Controllers.Base;
using ParkingATHWeb.Areas.Admin.ViewModels.AdminUser;
using ParkingATHWeb.Contracts.DTO.User;
using ParkingATHWeb.Contracts.Services.Base;

namespace ParkingATHWeb.Areas.Admin.Controllers
{
    public class AdminUserController : AdminServiceController<AdminUserListItemViewModel, AdminUserCreateViewModel, AdminUserEditViewModel, AdminUserDeleteViewModel, UserBaseDto, int>
    {
        public AdminUserController(IEntityService<UserBaseDto, int> entityService) : base(entityService)
        {
        }
    }
}
