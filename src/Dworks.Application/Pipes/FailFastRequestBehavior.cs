using FluentValidation.Results;
using FluentValidation;
using MediatR;
using System.Net;
using Dworks.Application.Responses.ApiResponse;
using System.Text;


namespace Dworks.Application.Pipes
{
    public class FailFastRequestBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
            where TRequest : IRequest<TResponse>
            where TResponse : DefaultResponse, new()
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public FailFastRequestBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);

            var failures = _validators
                .Select(v => v.Validate(context))
                .SelectMany(result => result.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Any())
            {
                return AddError(failures.First());
            }

            return await next();
        }

        private static TResponse AddError(ValidationFailure failure)
        {
            var responseType = typeof(TResponse);
            var response = Activator.CreateInstance(responseType) as TResponse;

            var validationResult = new ValidationResult();
            failure.ErrorCode = HttpStatusCode.UnprocessableEntity.ToString();
            failure.ErrorMessage = new StringBuilder(HttpStatusCode.UnprocessableEntity.ToString())
                .Append(": ")
                .Append(failure.ErrorMessage).ToString();
            validationResult.Errors.Add(failure);

            if (response != null)
                response.ValidationResult = validationResult;


            return response;
        }
    }
}
