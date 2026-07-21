using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IIdentityUnitOfWork
    {
        Task<int> SaveChangesAsync();
        IGenericIdentityRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : class;
    }
}
