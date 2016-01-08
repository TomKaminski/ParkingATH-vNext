using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.Mvc;
using ParkingATHWeb.Areas.Admin.Controllers.Base;
using ParkingATHWeb.Areas.Admin.ViewModels.GateUsage;
using ParkingATHWeb.Contracts.DTO.GateUsage;
using ParkingATHWeb.Contracts.Services;

namespace ParkingATHWeb.Areas.Admin.Controllers
{
    public class AdminGateUsageController : AdminServiceBaseController<AdminGateUsageListItemViewModel, GateUsageBaseDto, Guid>
    {
        private readonly IGateUsageService _entityService;

        public AdminGateUsageController(IGateUsageService entityService) : base(entityService)
        {
            _entityService = entityService;
        }

        public override async Task<IActionResult> List()
        {
            return View((await _entityService.GetAllAdminAsync()).Result.Select(Mapper.Map<AdminGateUsageListItemViewModel>));
        }
    }
}
