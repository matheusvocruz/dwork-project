using DWorks.Domain.Entities;

namespace Dworks.Application.Interfaces.UseCases
{
    public interface IProjectDeleteUseCase
    {
        void execute(Project project);
    }
}
