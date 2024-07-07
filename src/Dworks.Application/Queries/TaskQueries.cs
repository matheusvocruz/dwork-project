using AutoMapper;
using Dworks.Application.Interfaces.Queries;
using Dworks.Application.Responses.Report;
using Dworks.Application.Responses.Task;
using DWorks.Domain.Exceptions;
using DWorks.Domain.Interfaces.Repositories;

namespace Dworks.Application.Queries
{
    public class TaskQueries : ITaskQueries
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;

        public TaskQueries(ITaskRepository taskRepository,
            IMapper mapper) { 
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        public async Task<DWorks.Domain.Entities.Task> getById(long id)
        {
            try
            {
                return await _taskRepository.GetByIdAsync(id);
            }
            catch (Exception e)
            {
                throw new BadRequestException(e.Message);
            }
        }

        public async Task<bool> hasPendingTaskByProject(long projectId)
        {
            try
            {
                return await _taskRepository.hasPendingTaskByProject(projectId);
            }
            catch (Exception e)
            {
                throw new BadRequestException(e.Message);
            }
        }

        public async Task<int> getCountByProject(long projectId)
        {
            try
            {
                return await _taskRepository.getCountByProject(projectId);
            }
            catch (Exception e)
            {
                throw new BadRequestException(e.Message);
            }
        }

        public async Task<GetTasksResponse> getAllByProject(long projectId)
        {
            try
            {
                return new GetTasksResponse { Tasks = _mapper.Map<List<GetTaskResponse>>(await _taskRepository.getAllByProject(projectId)) };
            }
            catch (Exception e)
            {
                throw new BadRequestException(e.Message);
            }
        }

        public async Task<DoneTaskReportResponse> getDoneReport()
        {
            try
            {
                return new DoneTaskReportResponse { Report = _mapper.Map<List<DoneTask>>(await _taskRepository.getDoneReport()) };
            }
            catch (Exception e)
            {
                throw new BadRequestException(e.Message);
            }
        }
    }
}
