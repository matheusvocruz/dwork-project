using DWorks.Domain.Enums.Task;
using DWorks.Domain.Interfaces.Repositories;
using DWorks.Domain.ValueObjects;
using DWorks.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DWorks.Infra.Data.Repositories
{
    public class TaskRepository : BaseRepository<Domain.Entities.Task>, ITaskRepository
    {
        private readonly ProjectContext _context;

        public TaskRepository(ProjectContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> hasPendingTaskByProject(long projectId)
            => await GetQueryable().AnyAsync(x => x.ProjectId.Equals(projectId)
                    && x.Status.Equals(TaskStatusEnum.PENDING));

        public async Task<int> getCountByProject(long projectId)
            => await GetQueryable().CountAsync(x => x.ProjectId.Equals(projectId));

        public async Task<List<Domain.Entities.Task>> getAllByProject(long projectId)
            => await GetQueryable().Where(x => x.ProjectId.Equals(projectId)).ToListAsync();

        public async Task<List<DoneTaskDto>> getDoneReport()
        {
            FormattableString select = $@"select count(0) as Total, u.Id as UserId, u.Name as UserName
                    from Task t
                    inner join Project p on p.Id = t.ProjectId 
                    inner join `User` u on u.Id = p.UserId 
                    where t.Status = 3 
                    and t.CreatedAt BETWEEN CURDATE() - INTERVAL 30 DAY AND CURDATE()
                    group by u.Id";

            return await GetDatabase().SqlQuery<DoneTaskDto>(select).ToListAsync();
        }
    }
}
