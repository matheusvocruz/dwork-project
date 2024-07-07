using Dworks.Application.Requests.Project;
using FluentValidation;

namespace Dworks.Application.Validations.Projetct
{
    public class CreateProjectRequestValidation : AbstractValidator<CreateProjectRequest>
    {
        public CreateProjectRequestValidation()
        {
            RuleFor(c => c.Name)
                .NotNull();

            RuleFor(c => c.UserId)
                .NotNull()
                .GreaterThan(0);
        }
    }
}
