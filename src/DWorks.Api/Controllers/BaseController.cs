using Dworks.Application.Requests;
using Dworks.Application.Responses.ApiResponse;
using DWorks.Api.Attributes;
using DWorks.Infra.IoC;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using System.Net;

namespace DWorks.Api.Controllers
{
    [ApiController]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [CheckUserAttribute]
    public abstract class BaseController : ControllerBase
    {
        protected ActionResult CustomResponse(DefaultResponse result)
        {
            if (IsValidOperation(result))
                return Ok(result.Data);

            if (IsNoContent(result))
                return NoContent();

            var response = buildCustomResponse(
                GetIntStatusCodeByValidationResult(result.ValidationResult),
                result.ValidationResult.Errors.First().ErrorMessage
            );

            return StatusCode(response.Status, response);
        }

        private bool IsValidOperation(DefaultResponse result)
        {
            return result != null && result.ValidationResult.IsValid;
        }

        private bool IsNoContent(DefaultResponse result)
        {
            return result == null;
        }

        private static int GetIntStatusCodeByValidationResult(ValidationResult validationResult)
        {
            return (int)(HttpStatusCode)Enum.Parse(typeof(HttpStatusCode), validationResult.Errors.First().ErrorCode);
        }

        private CustomResponse buildCustomResponse(int status, string message)
            => new CustomResponse { Status = status, Message = message };
        protected void buildUserId(ApiRequest apiRequest)
        {
            if (Request.Headers["x-user-id"].IsNullOrEmpty())
            {
                throw new InvalidOperationException("x-user-id required");
            }

            apiRequest.UserId = Convert.ToInt32(Request.Headers["x-user-id"]);
        }
    }
}
