using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DWorks.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        DatabaseFacade GetDatabase();
        void Delete(TEntity entityToDelete);
        IQueryable<TEntity> GetQueryable();
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(long id);
        IQueryable<TEntity> Include();
        Task Insert(TEntity entity);
        Task Insert(List<TEntity> entities);
        Task Update(TEntity entityToUpdate);
    }
}
