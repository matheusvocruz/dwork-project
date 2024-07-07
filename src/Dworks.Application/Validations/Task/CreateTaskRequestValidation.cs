using Dworks.Application.Requests.Task;
using FluentValidation;

namespace Dworks.Application.Validations.Task
{
    public class CreateTaskRequestValidation : AbstractValidator<CreateTaskRequest>
    {
        public CreateTaskRequestValidation()
        {
            RuleFor(c => c.Tittle)
                .NotNull();

            RuleFor(c => c.Description)
                .NotNull();

            RuleFor(c => c.Priority)
                .NotNull()
                .IsInEnum();

            RuleFor(c => c.ProjectId)
                .NotNull()
                .GreaterThan(0);

            RuleFor(c => c.ExpiresAt)
                .NotNull();

            RuleFor(c => c.UserId)
                .NotNull()
                .GreaterThan(0);
        }
    }
}
