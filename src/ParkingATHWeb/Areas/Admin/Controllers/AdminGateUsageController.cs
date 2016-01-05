using System;
using ParkingATHWeb.Areas.Admin.Controllers.Base;
using ParkingATHWeb.Areas.Admin.ViewModels.GateUsage;
using ParkingATHWeb.Contracts.DTO.GateUsage;
using ParkingATHWeb.Contracts.Services.Base;

namespace ParkingATHWeb.Areas.Admin.Controllers
{
    public class AdminGateUsageController : AdminServiceBaseController<AdminGateUsageListItemViewModel, GateUsageBaseDto, Guid>
    {
        public AdminGateUsageController(IEntityService<GateUsageBaseDto, Guid> entityService) : base(entityService)
        {
        }
    }
}
