using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Persistence.Data.DbContexts;
using Persistence.IdentityData.DbContexts;
using Persistence.IdentityData.ImplementInterfaces;

namespace Persistence.Data.ImplementInterfaces
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppointmentDbContext _dbContext;
        private readonly Dictionary<Type, object> _repositories = [];

        public UnitOfWork(AppointmentDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : class
        {
            var EntityType = typeof(TEntity);
            if (_repositories.TryGetValue(EntityType, out object? repository))
            {
                return (IGenericRepository<TEntity, TKey>)repository;
            }
            var newRepo = new GenericRepository<TEntity, TKey>(_dbContext);
            _repositories[EntityType] = newRepo;
            return newRepo;
        }
        public async Task<int> SaveChangesAsync() => await _dbContext.SaveChangesAsync();
    }
}
