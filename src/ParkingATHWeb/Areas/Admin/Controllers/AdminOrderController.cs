using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.Mvc;
using ParkingATHWeb.ApiModels.Base;
using ParkingATHWeb.Areas.Admin.Controllers.Base;
using ParkingATHWeb.Areas.Admin.ViewModels.Order;
using ParkingATHWeb.Contracts.DTO.Order;
using ParkingATHWeb.Contracts.Services;

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

        public override async Task<IActionResult> List()
        {
            var serviceResult = await _entityService.GetAllAdminAsync();
            return Json(serviceResult.IsValid
                ? SmartJsonResult<IEnumerable<AdminOrderListItemViewModel>>.Success(serviceResult.Result.Select(_mapper.Map<AdminOrderListItemViewModel>))
                : SmartJsonResult<IEnumerable<AdminOrderListItemViewModel>>.Failure(serviceResult.ValidationErrors));
        }
    }
}
