using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.Mvc;
using ParkingATHWeb.ApiModels.Base;
using ParkingATHWeb.Contracts.Common;
using ParkingATHWeb.Contracts.Services.Base;
using ParkingATHWeb.ViewModels.Base;

namespace ParkingATHWeb.Areas.Admin.Controllers.Base
{
    public class AdminServiceBaseController<TListViewModel, TDto, TKeyType> : AdminBaseController
        where TListViewModel : SmartParkListBaseViewModel
        where TKeyType : struct
        where TDto : BaseDto<TKeyType>
    {
        private readonly IEntityService<TDto, TKeyType> _entityService;
        private readonly IMapper _mapper;

        public AdminServiceBaseController(IEntityService<TDto, TKeyType> entityService, IMapper mapper)
        {
            _entityService = entityService;
            _mapper = mapper;
        }

        [Route("~/[area]/[controller]")]
        public virtual IActionResult Index()
        {
            return PartialView();
        }

        public virtual async Task<IActionResult> ListAsync()
        {
            var serviceResult = await GetAllAsync();
            return Json(serviceResult.IsValid 
                ? SmartJsonResult<IEnumerable<TListViewModel>>.Success(serviceResult.Result.Select(_mapper.Map<TListViewModel>)) 
                : SmartJsonResult<IEnumerable<TListViewModel>>.Failure(serviceResult.ValidationErrors));
        }

        public virtual IActionResult List()
        {
            var serviceResult = _entityService.GetAll();
            return Json(serviceResult.IsValid
                ? SmartJsonResult<IEnumerable<TListViewModel>>.Success(serviceResult.Result.Select(_mapper.Map<TListViewModel>))
                : SmartJsonResult<IEnumerable<TListViewModel>>.Failure(serviceResult.ValidationErrors));
        }

        protected virtual async Task<ServiceResult<IEnumerable<TDto>>> GetAllAsync()
        {
            return await _entityService.GetAllAsync();
        }
    }
}
