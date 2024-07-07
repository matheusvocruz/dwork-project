using DWorks.Domain.Entities;
using DWorks.Domain.Interfaces.Repositories;
using DWorks.Infra.Data.Context;

namespace DWorks.Infra.Data.Repositories
{
    public class CommentRepository : BaseRepository<Comment>, ICommentRepository
    {
        private readonly ProjectContext _context;

        public CommentRepository(ProjectContext context) : base(context)
        {
            _context = context;
        }
    }
}
