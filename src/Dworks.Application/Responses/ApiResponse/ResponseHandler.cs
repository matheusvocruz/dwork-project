using DWorks.Domain.Exceptions;
using FluentValidation.Results;

namespace Dworks.Application.Responses.ApiResponse
{
    public class ResponseHandler<T> where T : class
    {
        protected ApiResponse<T> Response;

        protected ResponseHandler()
        {
            Response = new ApiResponse<T> { ValidationResult = new ValidationResult() };
        }

        protected ApiResponse<T> ThrowError(BaseException baseException)
        {
            var validationResult = new ValidationFailure(string.Empty, baseException.Message);
            validationResult.ErrorCode = baseException.StatusCode.ToString();
            Response.ValidationResult.Errors.Add(validationResult);

            return Response;
        }
    }
}
