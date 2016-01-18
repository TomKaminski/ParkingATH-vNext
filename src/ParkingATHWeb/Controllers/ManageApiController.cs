using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using ParkingATHWeb.ApiModels;
using ParkingATHWeb.ApiModels.Base;
using ParkingATHWeb.Contracts.Services;
using ParkingATHWeb.Infrastructure.Attributes;

namespace ParkingATHWeb.Controllers
{
    [Route("api/Manage")]
    [ApiHeaderAuthorize]
    public class ManageApiController : BaseApiController
    {
        private readonly IUserService _userService;

        public ManageApiController(IUserService userService) : base(userService)
        {
            _userService = userService;
        }

        [Route("ChangePassword")]
        [HttpPost]
        public async Task<ApiResult<bool>> ChangePassword([FromBody] ChangePasswordApiModel model)
        {
            if (!ModelState.IsValid)
                return ApiResult<bool>.Failure(GetModelStateErrors(ModelState));

            var changePasswordResult =
                await _userService.ChangePasswordAsync(model.Email, model.OldPassword, model.NewPassword);

            return changePasswordResult.IsValid
                ? ApiResult<bool>.Success(true)
                : ApiResult<bool>.Failure(changePasswordResult.ValidationErrors);
        }

        [Route("ChangeEmail")]
        [HttpPost]
        public async Task<ApiResult<bool>> ChangeEmail([FromBody] ChangeEmailApiModel model)
        {
            if (!ModelState.IsValid)
                return ApiResult<bool>.Failure(GetModelStateErrors(ModelState));

            var changeEmailResult = await _userService.ChangeEmailAsync(model.Email, model.NewEmail, model.Password);

            return changeEmailResult.IsValid
                ? ApiResult<bool>.Success(true)
                : ApiResult<bool>.Failure(changeEmailResult.ValidationErrors);
        }
    }
}
