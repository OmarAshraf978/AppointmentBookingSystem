using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Persistence.IdentityData.DbContexts;

namespace Persistence.IdentityData.ImplementInterfaces
{
    public class IdentityUnitOfWork : IIdentityUnitOfWork
    {
        private readonly AppointmentIdentityDbContext _dbContext;
        private readonly Dictionary<Type, object> _repositories = [];

        public IdentityUnitOfWork(AppointmentIdentityDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IGenericIdentityRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : class
        {
            var EntityType = typeof(TEntity);
            if (_repositories.TryGetValue(EntityType, out object? repository))
            {
                return (IGenericIdentityRepository<TEntity, TKey>)repository;
            }
            var newRepo = new GenericIdentityRepository<TEntity, TKey>(_dbContext);
            _repositories[EntityType] = newRepo;
            return newRepo;
        }
        public async Task<int> SaveChangesAsync() => await _dbContext.SaveChangesAsync();
    }
}
