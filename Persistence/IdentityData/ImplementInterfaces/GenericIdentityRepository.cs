using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Persistence.IdentityData.DbContexts;

namespace Persistence.IdentityData.ImplementInterfaces
{
    public class GenericIdentityRepository<TEntity, TKey> : IGenericIdentityRepository<TEntity, TKey> where TEntity : class
    {
        private readonly AppointmentIdentityDbContext _dbContext;

        public GenericIdentityRepository(AppointmentIdentityDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(TEntity entity)
        => await _dbContext.Set<TEntity>().AddAsync(entity);

        public void Delete(TEntity entity)
        => _dbContext.Set<TEntity>().Remove(entity);

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        => await _dbContext.Set<TEntity>().ToListAsync();

        public async Task<TEntity> GetByIdAsync(TKey id)
        => await _dbContext.Set<TEntity>().FindAsync(id);

        public void Update(TEntity entity)
        => _dbContext.Set<TEntity>().Update(entity);
    }
}
