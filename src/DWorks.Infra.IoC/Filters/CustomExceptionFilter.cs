using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace DWorks.Infra.IoC.Filters
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            var response = context.HttpContext.Response;

            response.StatusCode = StatusCodes.Status500InternalServerError;
            response.ContentType = "application/json";
            context.Result = new JsonResult(exception.Message);
        }
    }
}
