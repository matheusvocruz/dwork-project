using Dworks.Application.Interfaces.UseCases;
using DWorks.Domain.Exceptions;
using DWorks.Domain.Interfaces.Repositories;

namespace Dworks.Application.UseCases.Task
{
    public class TaskUpdateUseCase : ITaskUpdateUseCase
    {
        private readonly ITaskRepository _taskReposiroty;

        public TaskUpdateUseCase(ITaskRepository taskReposiroty)
        {
            _taskReposiroty = taskReposiroty;
        }

        public async Task<DWorks.Domain.Entities.Task> execute(DWorks.Domain.Entities.Task task)
        {
            try
            {
                await _taskReposiroty.Update(task);
                return task;
            }
            catch (Exception e)
            {
                throw new BadRequestException(e.Message);
            }
        }
    }
}
