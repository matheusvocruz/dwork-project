using FluentValidation.Results;

namespace Dworks.Application.Responses.ApiResponse
{
    public class DefaultResponse
    {
        public object Data { get; set; }
        public ValidationResult ValidationResult { get; set; }
    }
}
