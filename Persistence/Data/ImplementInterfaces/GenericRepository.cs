using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data.DbContexts;
using Persistence.IdentityData.DbContexts;

namespace Persistence.Data.ImplementInterfaces
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : class
    {
        private readonly AppointmentDbContext _dbContext;

        public GenericRepository(AppointmentDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(TEntity entity)
        => await _dbContext.Set<TEntity>().AddAsync(entity);

        public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        => _dbContext.Set<TEntity>().AnyAsync(predicate);

        public void Delete(TEntity entity)
        => _dbContext.Set<TEntity>().Remove(entity);

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        => await _dbContext.Set<TEntity>().ToListAsync();

        public async Task<IEnumerable<TEntity>> GetAllBySpecificColumnAsync(Expression<Func<TEntity, bool>> predicate)
        => await _dbContext.Set<TEntity>().Where(predicate).ToListAsync();

        public async Task<IEnumerable<TEntity>> GetAllBySpecificColumnWithIncludeAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return await query.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllWithIncludeAsync(params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return await query.ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(TKey id)
        => await _dbContext.Set<TEntity>().FindAsync(id);

        public async Task<TEntity> GetBySpecificColumnAsync(Expression<Func<TEntity, bool>> predicate)
        => await _dbContext.Set<TEntity>().FirstOrDefaultAsync(predicate);

        public async Task<TEntity?> GetBySpecificColumnWithIncludeAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return await query.FirstOrDefaultAsync(predicate);
        }

        public void Update(TEntity entity)
        => _dbContext.Set<TEntity>().Update(entity);
    }
}
