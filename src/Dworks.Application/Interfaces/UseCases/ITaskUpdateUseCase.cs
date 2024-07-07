namespace Dworks.Application.Interfaces.UseCases
{
    public interface ITaskUpdateUseCase
    {
        Task<DWorks.Domain.Entities.Task> execute(DWorks.Domain.Entities.Task task);
    }
}
