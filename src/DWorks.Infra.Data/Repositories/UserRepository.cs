using DWorks.Domain.Entities;
using DWorks.Domain.Interfaces.Repositories;
using DWorks.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DWorks.Infra.Data.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly ProjectContext _context;

        public UserRepository(ProjectContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> hasUserWithId(long id)
            => await GetQueryable().AnyAsync(x => x.Id.Equals(id));
    }
}
