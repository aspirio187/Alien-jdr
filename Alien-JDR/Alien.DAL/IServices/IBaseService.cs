using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.DAL.IServices
{
    public interface IBaseService<T>
        where T : class
    {
        ValueTask<T> GetEntityAsync(int id);
        ValueTask<IEnumerable<T>> GetEntitiesAsync();
        bool SaveChanges();
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
