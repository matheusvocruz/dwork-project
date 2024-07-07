using DWorks.Domain.Entities;
using DWorks.Domain.Interfaces.Repositories;
using DWorks.Infra.Data.Context;

namespace DWorks.Infra.Data.Repositories
{
    public class ProjectRepository : BaseRepository<Project>, IProjectRepository
    {
        private readonly ProjectContext _context;

        public ProjectRepository(ProjectContext context) : base(context)
        {
            _context = context;
        }
    }
}
