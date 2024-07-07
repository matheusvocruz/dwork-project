using Dworks.Application.Requests.Task;
using FluentValidation;

namespace Dworks.Application.Validations.Task
{
    public class CreateTaskCommentRequestValidation : AbstractValidator<CreateTaskCommentRequest>
    {
        public CreateTaskCommentRequestValidation()
        {
            RuleFor(c => c.TaskId)
                .NotNull()
                .GreaterThan(0);

            RuleFor(c => c.Comment)
                .NotNull();

            RuleFor(c => c.UserId)
                .NotNull()
                .GreaterThan(0);
        }
    }
}
