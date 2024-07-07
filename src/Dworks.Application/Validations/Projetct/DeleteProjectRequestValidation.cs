using Dworks.Application.Requests.Project;
using FluentValidation;

namespace Dworks.Application.Validations.Projetct
{
    public class DeleteProjectRequestValidation : AbstractValidator<DeleteProjectRequest>
    {
        public DeleteProjectRequestValidation()
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
