using DWorks.Domain.ValueObjects;

namespace DWorks.Domain.Interfaces.Repositories
{
    public interface ITaskRepository : IBaseRepository<Entities.Task>
    {
        Task<bool> hasPendingTaskByProject(long projectId);
        Task<int> getCountByProject(long projectId);
        Task<List<Entities.Task>> getAllByProject(long projectId);
        Task<List<DoneTaskDto>> getDoneReport();
    }
}
