using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.Mvc;
using ParkingATHWeb.ApiModels.Base;
using ParkingATHWeb.Areas.Admin.Controllers.Base;
using ParkingATHWeb.Areas.Admin.ViewModels.GateUsage;
using ParkingATHWeb.Contracts.DTO.GateUsage;
using ParkingATHWeb.Contracts.Services;

namespace ParkingATHWeb.Areas.Admin.Controllers
{
    public class AdminGateUsageController : AdminServiceBaseController<AdminGateUsageListItemViewModel, GateUsageBaseDto, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IGateUsageService _service;

        public AdminGateUsageController(IGateUsageService entityService, IMapper mapper) : base(entityService, mapper)
        {
            _mapper = mapper;
            _service = entityService;
        }

        public override async Task<IActionResult> ListAsync()
        {
            var serviceResult = await _service.GetAllAdminAsync();
            return Json(serviceResult.IsValid
                ? SmartJsonResult<IEnumerable<AdminGateUsageListItemViewModel>>.Success(serviceResult.Result.Select(_mapper.Map<AdminGateUsageListItemViewModel>))
                : SmartJsonResult<IEnumerable<AdminGateUsageListItemViewModel>>.Failure(serviceResult.ValidationErrors));
        }
    }
}
