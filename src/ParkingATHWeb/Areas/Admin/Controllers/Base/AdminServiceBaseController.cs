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
    public class AdminServiceBaseController<TListViewModel, TDto, TKeyType> : AdminBaseController
        where TListViewModel : ParkingAthListBaseViewModel
        where TKeyType : struct
        where TDto : BaseDto<TKeyType>
    {
        private readonly IEntityService<TDto, TKeyType> _entityService;

        public AdminServiceBaseController(IEntityService<TDto, TKeyType> entityService)
        {
            _entityService = entityService;
        }

        [HttpGet]
        public virtual async Task<IActionResult> List()
        {
            return View((await _entityService.GetAllAsync()).Result.Select(Mapper.Map<TListViewModel>));
        }
    }
}
