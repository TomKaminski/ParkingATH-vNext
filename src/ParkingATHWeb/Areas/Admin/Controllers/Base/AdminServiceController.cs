using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.Mvc;
using ParkingATHWeb.ApiModels.Base;
using ParkingATHWeb.Contracts.Common;
using ParkingATHWeb.Contracts.Services.Base;
using ParkingATHWeb.ViewModels.Base;

namespace ParkingATHWeb.Areas.Admin.Controllers.Base
{
    [Route("[area]/[controller]/[action]")]
    public class AdminServiceController<TListViewModel, TCreateViewModel, TEditViewModel, TDeleteViewModel, TDto, TKeyType> : AdminServiceBaseController<TListViewModel, TDto, TKeyType>
        where TListViewModel : SmartParkListBaseViewModel
        where TCreateViewModel : SmartParkCreateBaseViewModel
        where TEditViewModel : SmartParkEditBaseViewModel<TKeyType>
        where TKeyType : struct
        where TDeleteViewModel : SmartParkDeleteBaseViewModel<TKeyType>
        where TDto : BaseDto<TKeyType>
    {
        private readonly IEntityService<TDto, TKeyType> _entityService;
        private readonly IMapper _mapper;

        public AdminServiceController(IEntityService<TDto, TKeyType> entityService, IMapper mapper) : base(entityService, mapper)
        {
            _entityService = entityService;
            _mapper = mapper;
        }

        //[HttpGet]
        //public virtual IActionResult Create()
        //{
        //    return View();
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> Create(TCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var serviceResult = await _entityService.CreateAsync(_mapper.Map<TDto>(model));
                return Json(serviceResult.IsValid 
                    ? SmartJsonResult<TListViewModel>.Success(_mapper.Map<TListViewModel>(serviceResult.Result)) 
                    : SmartJsonResult.Failure(serviceResult.ValidationErrors));
            }
            return Json(SmartJsonResult.Failure(GetModelStateErrors(ModelState)));
        }

        //[HttpGet]
        //[Route("{id}")]
        //public virtual async Task<IActionResult> Delete(TKeyType id)
        //{
        //    return View(_mapper.Map<TListViewModel>(await _entityService.GetAsync(id)));
        //}

        [HttpPost]
        [Route("{id}")]
        [ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> Delete(TDeleteViewModel model)
        {
            if (ModelState.IsValid)
            {
                var serviceResult = await _entityService.DeleteAsync(model.Id);
                return Json(serviceResult.IsValid
                    ? SmartJsonResult.Success()
                    : SmartJsonResult.Failure(serviceResult.ValidationErrors));
            }
            return Json(SmartJsonResult.Failure(GetModelStateErrors(ModelState)));
        }

        //[HttpGet]
        //[Route("{id}")]
        //public virtual async Task<IActionResult> Edit(TKeyType id)
        //{
        //    return View(_mapper.Map<TEditViewModel>(await _entityService.GetAsync(id)));
        //}

        [HttpPost]
        [Route("{id}")]
        [ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> Edit(TEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var serviceResult = await _entityService.EditAsync(_mapper.Map<TDto>(model));
                return Json(serviceResult.IsValid
                    ? SmartJsonResult<TListViewModel>.Success(_mapper.Map<TListViewModel>(serviceResult.Result))
                    : SmartJsonResult.Failure(serviceResult.ValidationErrors));
            }
            return Json(SmartJsonResult.Failure(GetModelStateErrors(ModelState)));
        }

        //protected IActionResult ReturnWithModelBase<TBaseModel>(TBaseModel model, ServiceResult serviceResult, ModelStateDictionary modelstate, string returnActionName = "List")
        //    where TBaseModel : SmartParkBaseViewModel
        //{
        //    return serviceResult.IsValid
        //        ? RedirectToAction(returnActionName)
        //        : ReturnModelWithError(model, serviceResult, modelstate);
        //}
    }
}
