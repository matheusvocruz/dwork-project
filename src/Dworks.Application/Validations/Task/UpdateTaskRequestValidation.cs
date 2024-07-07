using Dworks.Application.Requests.Task;
using FluentValidation;

namespace Dworks.Application.Validations.Task
{
    public class UpdateTaskRequestValidation : AbstractValidator<UpdateTaskRequest>
    {
        public UpdateTaskRequestValidation()
        {
            RuleFor(c => c.Id)
                .NotNull()
                .GreaterThan(0);

            RuleFor(c => c.Tittle)
                .NotNull();

            RuleFor(c => c.Description)
                .NotNull();

            RuleFor(c => c.Status)
                .IsInEnum();

            RuleFor(c => c.ExpiresAt)
                .NotNull();

            RuleFor(c => c.UserId)
                .NotNull()
                .GreaterThan(0);
        }
    }
}
