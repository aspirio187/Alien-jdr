using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.DAL.Interfaces
{
    public interface IRepositoryBase<T>
        where T : class
    {
        Task<T> GetEntityAsync(int id);
        Task<IEnumerable<T>> GetEntitiesAsync();
        bool SaveChanges();
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
