using DWorks.Domain.Entities;
using DWorks.Domain.Interfaces.Repositories;
using DWorks.Infra.Data.Context;

namespace DWorks.Infra.Data.Repositories
{
    public class LogRepository : BaseRepository<Log>, ILogRepository
    {
        private readonly ProjectContext _context;

        public LogRepository(ProjectContext context) : base(context)
        {
            _context = context;
        }
    }
}
