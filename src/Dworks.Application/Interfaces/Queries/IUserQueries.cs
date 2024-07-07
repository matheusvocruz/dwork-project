using DWorks.Domain.Entities;

namespace Dworks.Application.Interfaces.Queries
{
    public interface IUserQueries
    {
        Task<bool> hasUserWithId(long id);
        Task<User> getById(long id);
    }
}
