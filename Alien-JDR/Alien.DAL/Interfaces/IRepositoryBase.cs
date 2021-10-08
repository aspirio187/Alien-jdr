using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.DAL.Interfaces
{
    public interface IRepositoryBase<TEntity, TKey>
        where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByKeyAsync(TKey key);
        TEntity Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TKey key);
        bool SaveChanges();
    }
}
