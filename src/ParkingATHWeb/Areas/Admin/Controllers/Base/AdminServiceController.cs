using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.Mvc;
using ParkingATHWeb.Contracts.Common;
using ParkingATHWeb.Contracts.Services.Base;
using ParkingATHWeb.ViewModels.Base;

namespace ParkingATHWeb.Areas.Admin.Controllers.Base
{
    [Route("[area]/[controller]/[action]")]
    public class AdminServiceController<TListViewModel, TCreateViewModel, TEditViewModel, TDeleteViewModel, TDto, TKeyType> : AdminServiceBaseController<TListViewModel, TDto, TKeyType>
        where TListViewModel : ParkingAthListBaseViewModel
        where TCreateViewModel : ParkingAthCreateBaseViewModel
        where TEditViewModel : ParkingAthEditBaseViewModel<TKeyType>
        where TKeyType : struct
        where TDeleteViewModel : ParkingAthDeleteBaseViewModel<TKeyType>
        where TDto : BaseDto<TKeyType>
    {
        private readonly IEntityService<TDto, TKeyType> _entityService;

        public AdminServiceController(IEntityService<TDto, TKeyType> entityService) : base(entityService)
        {
            _entityService = entityService;
        }

        [HttpGet]
        public virtual IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> Create(TCreateViewModel model)
        {
            var serviceResult = await _entityService.CreateAsync(Mapper.Map<TDto>(model));
            return ReturnWithModelBase(model, serviceResult);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual async Task<IActionResult> Delete(TKeyType id)
        {
            return View(Mapper.Map<TListViewModel>(await _entityService.GetAsync(id)));
        }

        [HttpPost]
        [Route("{id}")]
        [ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> Delete(TDeleteViewModel model)
        {
            var serviceResult = await _entityService.DeleteAsync(model.Id);
            return ReturnWithModelBase(model, serviceResult);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual async Task<IActionResult> Edit(TKeyType id)
        {
            return View(Mapper.Map<TEditViewModel>(await _entityService.GetAsync(id)));
        }

        [HttpPost]
        [Route("{id}")]
        [ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> Edit(TEditViewModel model)
        {
            var serviceResult = await _entityService.EditAsync(Mapper.Map<TDto>(model));
            return ReturnWithModelBase(model, serviceResult);
        }

        protected IActionResult ReturnWithModelBase<TBaseModel>(TBaseModel model, ServiceResult serviceResult, string returnActionName = "List")
            where TBaseModel : ParkingAthBaseViewModel
        {
            return serviceResult.IsValid
                ? RedirectToAction(returnActionName)
                : ReturnModelWithError(model, serviceResult);
        }


    }
}
