using ParkingATHWeb.Areas.Admin.Controllers.Base;
using ParkingATHWeb.Areas.Admin.ViewModels.Order;
using ParkingATHWeb.Contracts.DTO.Order;
using ParkingATHWeb.Contracts.Services.Base;

namespace ParkingATHWeb.Areas.Admin.Controllers
{
    public class AdminOrderController : AdminServiceBaseController<AdminOrderListItemViewModel, OrderBaseDto, long>
    {
        public AdminOrderController(IEntityService<OrderBaseDto, long> entityService) : base(entityService)
        {
        }
    }
}
