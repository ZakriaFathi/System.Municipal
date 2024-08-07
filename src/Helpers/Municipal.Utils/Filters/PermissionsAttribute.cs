using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using Municipal.Utils.Enums;

namespace Municipal.Utils.Filters
{
    public class PermissionsAttribute : Attribute, IAsyncActionFilter
    {
        private readonly PermissionNames[] _allowedPermissions;

        public PermissionsAttribute(params PermissionNames[] allowedUserPermissions)
        {
            _allowedPermissions = allowedUserPermissions;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var permissionsClaim = context.HttpContext.User.FindFirstValue("permissions").Split(',');

            if (!permissionsClaim.IsNullOrEmpty())
            {
                if (!_allowedPermissions.Any(allowedPermission => permissionsClaim.Any(x => x.ToString() == allowedPermission.ToString("D"))))
                {
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    context.HttpContext.Response.ContentType = "text/plain";
                    await context.HttpContext.Response.WriteAsync("Forbidden");
                    return;
                }
                await next();
            }
            else
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                context.HttpContext.Response.ContentType = "text/plain";
                await context.HttpContext.Response.WriteAsync("Unauthorized");
            }
        }
    }
}
