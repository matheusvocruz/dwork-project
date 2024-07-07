using DWorks.Domain.Entities;

namespace Dworks.Application.Interfaces.UseCases
{
    public interface IProjectCreateUseCase
    {
        Task<Project> execute(Project project);
    }
}
