using System.Threading.Tasks;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using ParkingATHWeb.ApiModels;
using ParkingATHWeb.ApiModels.Account;
using ParkingATHWeb.ApiModels.Base;
using ParkingATHWeb.Contracts.DTO.User;
using ParkingATHWeb.Contracts.Services;

namespace ParkingATHWeb.Controllers
{
    [Route("api/Account")]
    public class AccountApiController : BaseApiController
    {
        private readonly IUserService _userService;

        public AccountApiController(IUserService userService) : base(userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ApiResult<UserBaseDto>> Login([FromBody] LoginApiModel model)
        {
            if (!ModelState.IsValid)
                return ApiResult<UserBaseDto>.Failure(GetModelStateErrors(ModelState));

            var loginApiResult = await _userService.LoginAsync(model.Username, model.Password);

            return loginApiResult.IsValid
                ? ApiResult<UserBaseDto>.Success(loginApiResult.Result)
                : ApiResult<UserBaseDto>.Failure(loginApiResult.ValidationErrors);
        }

        [HttpPost]
        [Route("Forgot")]
        public async Task<ApiResult<bool>> ForgotPassword([FromBody] ForgotApiModel model)
        {
            if (!ModelState.IsValid)
                return ApiResult<bool>.Failure(GetModelStateErrors(ModelState));

            var loginApiResult = await _userService.GetPasswordChangeTokenAsync(model.Email);

            return loginApiResult.IsValid
                ? ApiResult<bool>.Success(true)
                : ApiResult<bool>.Failure(loginApiResult.ValidationErrors);
        }



        #region TokenAuth - OBSOLETE
        ///// <summary>
        ///// Request a new token for a given username/password pair.
        ///// </summary>
        ///// <param name="req"></param>
        ///// <returns></returns>
        //[HttpPost]
        //[AllowAnonymous]
        //[Route("Login")]
        //public async Task<dynamic> Login([FromBody] AuthRequest req)
        //{
        //    var loginApiResult = await _userService.LoginAsync(req.Username, req.Password);
        //    // Obviously, at this point you need to validate the username and password against whatever system you wish.
        //    if (loginApiResult.IsValid)
        //    {
        //        DateTime? expires = DateTime.UtcNow.AddMinutes(10);
        //        var token = await GetTokenAsync(req.Username, expires);
        //        return new { authenticated = true, entityId = 1, token, tokenExpires = expires };
        //    }
        //    return new { authenticated = false };
        //}

        //[HttpGet]
        //[Route("IsAuthenticated")]
        //public async Task<dynamic> IsAuthenticated()
        //{
        //    var authenticated = false;
        //    string user = null;
        //    var entityId = -1;
        //    string token = null;
        //    var tokenExpires = default(DateTime?);

        //    if (CurrentUser != null)
        //    {
        //        authenticated = CurrentUser.Identity.IsAuthenticated;
        //        if (authenticated)
        //        {
        //            user = CurrentUser.Identity.Name;
        //            foreach (var c in CurrentUser.Claims)
        //            {
        //                if (c.Type == "EntityID") entityId = Convert.ToInt32(c.Value);
        //            }
        //            tokenExpires = DateTime.UtcNow.AddMinutes(2);
        //            token = await GetTokenAsync(CurrentUser.Email, tokenExpires);
        //        }
        //    }
        //    return new {authenticated, user, entityId, token, tokenExpires };
        //}
        #endregion
    }
}
