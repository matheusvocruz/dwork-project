namespace Dworks.Application.Interfaces.UseCases
{
    public interface ITaskDeleteUseCase
    {
        void execute(DWorks.Domain.Entities.Task task);
    }
}
