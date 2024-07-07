namespace Dworks.Application.Interfaces.UseCases
{
    public interface ITaskCreateUseCase
    {
        Task<DWorks.Domain.Entities.Task> execute(DWorks.Domain.Entities.Task taskk);
    }
}
