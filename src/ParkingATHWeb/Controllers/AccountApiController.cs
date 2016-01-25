using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using ParkingATHWeb.ApiModels;
using ParkingATHWeb.ApiModels.Account;
using ParkingATHWeb.ApiModels.Base;
using ParkingATHWeb.Contracts.DTO.User;
using ParkingATHWeb.Contracts.Services;
using ParkingATHWeb.Shared.Enums;

namespace ParkingATHWeb.Controllers
{
    [Route("api/Account")]
    public class AccountApiController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly IMessageService _messageService;

        public AccountApiController(IUserService userService, IMessageService messageService) : base(userService)
        {
            _userService = userService;
            _messageService = messageService;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<SmartJsonResult<UserBaseDto>> Login([FromBody] LoginApiModel model)
        {
            if (!ModelState.IsValid)
                return SmartJsonResult<UserBaseDto>.Failure(GetModelStateErrors(ModelState));

            var loginApiResult = await _userService.LoginAsync(model.Username, model.Password);

            return loginApiResult.IsValid
                ? SmartJsonResult<UserBaseDto>.Success(loginApiResult.Result)
                : SmartJsonResult<UserBaseDto>.Failure(loginApiResult.ValidationErrors);
        }

        [HttpPost]
        [Route("Forgot")]
        public async Task<SmartJsonResult<bool>> ForgotPassword([FromBody] ForgotApiModel model)
        {
            if (!ModelState.IsValid)
                return SmartJsonResult<bool>.Failure(GetModelStateErrors(ModelState));

            var changePasswordTokenResult = await _userService.GetPasswordChangeTokenAsync(model.Email);
            var changePasswordUrl = $"{Url.Action("RedirectFromToken", "Token", null, "http")}?id={changePasswordTokenResult.SecondResult}";
            await _messageService.SendMessageAsync(EmailType.ResetPassword, changePasswordTokenResult.Result, GetAppBaseUrl(),
                new Dictionary<string, string> { { "ChangePasswordLink", changePasswordUrl } });

            return changePasswordTokenResult.IsValid
                ? SmartJsonResult<bool>.Success(true)
                : SmartJsonResult<bool>.Failure(changePasswordTokenResult.ValidationErrors);
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
