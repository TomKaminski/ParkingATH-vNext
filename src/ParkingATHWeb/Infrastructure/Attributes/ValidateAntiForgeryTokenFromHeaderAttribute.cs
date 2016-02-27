using System;
using Microsoft.AspNet.Antiforgery;
using Microsoft.AspNet.Mvc.Filters;
using Microsoft.Extensions.OptionsModel;

namespace ParkingATHWeb.Infrastructure.Attributes
{
    public class ValidateAntiForgeryTokenFromHeaderAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            if (actionContext == null) throw new ArgumentNullException(nameof(actionContext));

            base.OnActionExecuting(actionContext);

            var antiForgery = actionContext.HttpContext.ApplicationServices.GetService(typeof(IAntiforgery)) as IAntiforgery;
            var options = actionContext.HttpContext.ApplicationServices.GetService(typeof(IOptions<AntiforgeryOptions>)) as IOptions<AntiforgeryOptions>;
            var config = options.Value;

            var request = actionContext.HttpContext.Request;

            var cookieToken = request.Cookies[config.CookieName];
            var formToken = request.Headers["X-XSRF-Token"];
            antiForgery.ValidateTokens(actionContext.HttpContext, new AntiforgeryTokenSet(formToken, cookieToken));
        }
    }
}
