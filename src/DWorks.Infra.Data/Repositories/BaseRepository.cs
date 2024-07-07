using DWorks.Domain.Entities;
using DWorks.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DWorks.Infra.Data.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public BaseRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public DatabaseFacade GetDatabase()
            => _context.Database;

        public void Delete(TEntity entityToDelete)
        {
            _context.Remove(entityToDelete);
            _context.SaveChanges();
        }

        public virtual IQueryable<TEntity> Include()
        {
            return _dbSet.AsQueryable();
        }

        public IQueryable<TEntity> GetQueryable()
        {
            return Include();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Include().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(long id)
        {
            return await Include().FirstOrDefaultAsync(c => c.Id == id);
        }

        public async System.Threading.Tasks.Task Insert(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task Insert(List<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task Update(TEntity entityToUpdate)
        {
            _dbSet.Update(entityToUpdate);
            await _context.SaveChangesAsync();
        }
    }
}
