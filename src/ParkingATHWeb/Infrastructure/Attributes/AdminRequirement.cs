using Microsoft.AspNet.Authorization;
using ParkingATHWeb.Models;

namespace ParkingATHWeb.Infrastructure.Attributes
{
    public class AdminRequirement : AuthorizationHandler<AdminRequirement>, IAuthorizationRequirement
    {
        protected override void Handle(AuthorizationContext context, AdminRequirement requirement)
        {
            var user = context.User == null ? new AppUserState() : new AppUserState(context.User);
            if (user.IsAdmin)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
        }
    }
}
