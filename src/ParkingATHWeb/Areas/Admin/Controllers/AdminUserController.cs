﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.Mvc;
using ParkingATHWeb.ApiModels.Base;
using ParkingATHWeb.Areas.Admin.Controllers.Base;
using ParkingATHWeb.Areas.Admin.ViewModels.User;
using ParkingATHWeb.Contracts.DTO.User;
using ParkingATHWeb.Contracts.Services;
using System.Linq;
using ParkingATHWeb.Infrastructure.Attributes;

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

        public override Task<IActionResult> Create(AdminUserCreateViewModel model)
        {
            throw new NotImplementedException();
        }

        public override IActionResult List()
        {
            var serviceResult = _entityService.GetAllAdmin();
            return Json(serviceResult.IsValid
                ? SmartJsonResult<IEnumerable<AdminUserListItemViewModel>>.Success(serviceResult.Result.Select(_mapper.Map<AdminUserListItemViewModel>))
                : SmartJsonResult<IEnumerable<AdminUserListItemViewModel>>.Failure(serviceResult.ValidationErrors));
        }

        [ValidateAntiForgeryTokenFromHeader]
        [HttpPost]
        public async Task<IActionResult> RecoverUser([FromBody]AdminUserDeleteViewModel model)
        {
            if (ModelState.IsValid)
            {
                var recoverUserResult = await _entityService.RecoverUserAsync(model.Id);
                return Json(recoverUserResult.IsValid
                    ? SmartJsonResult.Success("Operacja przywrócenia użytkownika zakończona pomyślnie.")
                    : SmartJsonResult.Failure(recoverUserResult.ValidationErrors));
            }
            return Json(SmartJsonResult.Failure(GetModelStateErrors(ModelState)));
        }

        [ValidateAntiForgeryTokenFromHeader]
        [HttpPost]
        public override async Task<IActionResult> Edit([FromBody]AdminUserEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var serviceResult = await _entityService.AdminEditAsync(_mapper.Map<UserBaseDto>(model), model.OldEmail);
                return Json(serviceResult.IsValid
                    ? SmartJsonResult.Success("Edycja użytkownika zakończona pomyślnie")
                    : SmartJsonResult.Failure(serviceResult.ValidationErrors));
            }
            return Json(SmartJsonResult.Failure(GetModelStateErrors(ModelState)));
        }
    }
}
