using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.Mvc;
using ParkingATHWeb.ApiModels.Base;
using ParkingATHWeb.Areas.Admin.Controllers.Base;
using ParkingATHWeb.Areas.Admin.ViewModels.GateUsage;
using ParkingATHWeb.Areas.Admin.ViewModels.Order;
using ParkingATHWeb.Contracts.DTO.Order;
using ParkingATHWeb.Contracts.Services;
using ParkingATHWeb.Infrastructure.Attributes;
using ParkingATHWeb.ViewModels.Base;

namespace ParkingATHWeb.Areas.Admin.Controllers
{
    public class AdminOrderController : AdminServiceBaseController<AdminOrderListItemViewModel, OrderBaseDto, long>
    {
        private readonly IOrderService _entityService;
        private readonly IMapper _mapper;

        public AdminOrderController(IOrderService entityService, IMapper mapper) : base(entityService, mapper)
        {
            _entityService = entityService;
            _mapper = mapper;
        }

        public override async Task<IActionResult> ListAsync()
        {
            var serviceResult = await _entityService.GetAllAdminAsync();
            return Json(serviceResult.IsValid
                ? SmartJsonResult<IEnumerable<AdminOrderListItemViewModel>>.Success(serviceResult.Result.Select(_mapper.Map<AdminOrderListItemViewModel>))
                : SmartJsonResult<IEnumerable<AdminOrderListItemViewModel>>.Failure(serviceResult.ValidationErrors));
        }

        [HttpPost]
        [ValidateAntiForgeryTokenFromHeader]
        public async Task<IActionResult> DateRangeListAsync(SmartParkListDateRangeRequestViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.DateFrom = DateTime.Today.AddDays(-6);
                model.DateTo = DateTime.Now;
            }

            var serviceResult = await _entityService.GetAllAdminAsync(x => x.Date >= model.DateFrom && x.Date <= model.DateTo);
            return Json(serviceResult.IsValid
                ? SmartJsonResult<SmartParkListWithDateRangeViewModel<AdminOrderListItemViewModel>>
                .Success(new SmartParkListWithDateRangeViewModel<AdminOrderListItemViewModel>
                {
                    ListItems = serviceResult.Result.Select(_mapper.Map<AdminOrderListItemViewModel>),
                    DateTo = model.DateTo,
                    DateFrom = model.DateFrom
                })
                : SmartJsonResult<SmartParkListWithDateRangeViewModel<AdminOrderListItemViewModel>>.Failure(serviceResult.ValidationErrors));
        }
    }
}
