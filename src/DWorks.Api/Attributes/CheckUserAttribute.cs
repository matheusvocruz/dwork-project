

using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Ocsp;

namespace DWorks.Api.Attributes
{
    public class CheckUserAttribute : Attribute, IActionFilter
    {
        public CheckUserAttribute()
        {
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Request.Headers["x-user-id"].IsNullOrEmpty())
            {
                throw new InvalidOperationException("x-user-id required");
            }
        }
    }
}
