using Microsoft.AspNetCore.Mvc.Filters;

namespace JWTAPI.Authorization
{
    [AttributeUsage(AttributeTargets.Class| AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // authorization
            var user = (User)context.HttpContext.Items["User"];
            if (user == null) 
            {
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}
