using Dworks.Application.Interfaces.UseCases;
using DWorks.Domain.Exceptions;
using DWorks.Domain.Interfaces.Repositories;

namespace Dworks.Application.UseCases.Task
{
    public class TaskDeleteUseCase : ITaskDeleteUseCase
    {
        private readonly ITaskRepository _taskReposiroty;

        public TaskDeleteUseCase(ITaskRepository taskReposiroty)
        {
            _taskReposiroty = taskReposiroty;
        }

        public void execute(DWorks.Domain.Entities.Task task)
        {
            try
            {
                _taskReposiroty.Delete(task);
            }
            catch (Exception e)
            {
                throw new BadRequestException(e.Message);
            }
        }
    }
}
