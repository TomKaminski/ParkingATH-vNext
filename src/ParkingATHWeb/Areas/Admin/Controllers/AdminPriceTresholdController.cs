using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.Mvc;
using ParkingATHWeb.ApiModels.Base;
using ParkingATHWeb.Areas.Admin.Controllers.Base;
using ParkingATHWeb.Areas.Admin.ViewModels.PriceTreshold;
using ParkingATHWeb.Contracts.DTO.PriceTreshold;
using ParkingATHWeb.Contracts.Services;
using System.Linq;

namespace ParkingATHWeb.Areas.Admin.Controllers
{
    public class AdminPriceTresholdController : AdminServiceController
                                                <AdminPriceTresholdListItemViewModel, 
                                                 AdminPriceTresholdCreateViewModel, 
                                                 AdminPriceTresholdEditViewModel, 
                                                 AdminPriceTresholdDeleteViewModel, 
                                                 PriceTresholdBaseDto, int>
    {
        private readonly IPriceTresholdService _entityService;
        private readonly IMapper _mapper;

        public AdminPriceTresholdController(IPriceTresholdService entityService, IMapper mapper) : base(entityService, mapper)
        {
            _entityService = entityService;
            _mapper = mapper;
        }

        public override async Task<IActionResult> List()
        {
            var serviceResult = await _entityService.GetAllAdminAsync();
            return Json(serviceResult.IsValid
                ? SmartJsonResult<IEnumerable<AdminPriceTresholdListItemViewModel>>.Success(serviceResult.Result.Select(_mapper.Map<AdminPriceTresholdListItemViewModel>))
                : SmartJsonResult<IEnumerable<AdminPriceTresholdListItemViewModel>>.Failure(serviceResult.ValidationErrors));
        }
    }
}
