using Dworks.Application.Requests.Task;
using FluentValidation;

namespace Dworks.Application.Validations.Task
{
    public class DeleteTaskRequestValidation : AbstractValidator<DeleteTaskRequest>
    {
        public DeleteTaskRequestValidation()
        {
            RuleFor(c => c.Id)
                .NotNull()
                .GreaterThan(0);

            RuleFor(c => c.UserId)
                .NotNull()
                .GreaterThan(0);
        }
    }
}
