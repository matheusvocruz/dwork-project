using DWorks.Domain.Entities;

namespace DWorks.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<bool> hasUserWithId(long id);
    }
}
