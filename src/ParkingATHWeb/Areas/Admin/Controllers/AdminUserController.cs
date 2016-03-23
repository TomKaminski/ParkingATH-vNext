using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.Mvc;
using ParkingATHWeb.ApiModels.Base;
using ParkingATHWeb.Areas.Admin.Controllers.Base;
using ParkingATHWeb.Areas.Admin.ViewModels.User;
using ParkingATHWeb.Business.Services;
using ParkingATHWeb.Contracts.DTO.User;
using ParkingATHWeb.Contracts.Services;

namespace ParkingATHWeb.Areas.Admin.Controllers
{
    public class AdminUserController : AdminServiceController<AdminUserListItemViewModel, AdminUserCreateViewModel, AdminUserEditViewModel, AdminUserDeleteViewModel, UserBaseDto, int>
    {
        private readonly IUserService _entityService;
        private readonly IMapper _mapper;

        public AdminUserController(IUserService entityService, IMapper mapper) : base(entityService, mapper)
        {
            _entityService = entityService;
            _mapper = mapper;
        }

        public override async Task<IActionResult> Create(AdminUserCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var serviceResult = await _entityService.CreateAsync(_mapper.Map<UserBaseDto>(model));
                return Json(serviceResult.IsValid
                    ? SmartJsonResult<AdminUserListItemViewModel>.Success(_mapper.Map<AdminUserListItemViewModel>(serviceResult.Result))
                    : SmartJsonResult.Failure(serviceResult.ValidationErrors));
            }
            return Json(SmartJsonResult.Failure(GetModelStateErrors(ModelState)));
        }
    }
}
