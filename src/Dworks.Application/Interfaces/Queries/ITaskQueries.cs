using Dworks.Application.Responses.Report;
using Dworks.Application.Responses.Task;

namespace Dworks.Application.Interfaces.Queries
{
    public interface ITaskQueries
    {
        Task<DWorks.Domain.Entities.Task> getById(long id);
        Task<bool> hasPendingTaskByProject(long projectId);
        Task<int> getCountByProject(long projectId);
        Task<GetTasksResponse> getAllByProject(long projectId);
        Task<DoneTaskReportResponse> getDoneReport();
    }
}
