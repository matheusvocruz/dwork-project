using Dworks.Application.Interfaces.UseCases;
using DWorks.Domain.Exceptions;
using DWorks.Domain.Interfaces.Repositories;
using DWorks.Infra.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dworks.Application.UseCases.Task
{
    public class TaskCreateUseCase : ITaskCreateUseCase
    {
        private readonly ITaskRepository _taskReposiroty;

        public TaskCreateUseCase(ITaskRepository taskReposiroty)
        {
            _taskReposiroty = taskReposiroty;
        }

        public async Task<DWorks.Domain.Entities.Task> execute(DWorks.Domain.Entities.Task task)
        {
            try
            {
                await _taskReposiroty.Insert(task);
                return task;
            }
            catch (Exception e)
            {
                throw new BadRequestException(e.Message);
            }
        }
    }
}
