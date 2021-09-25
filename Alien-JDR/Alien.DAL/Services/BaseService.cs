using Alien.DAL.IServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.DAL.Services
{
    public class BaseService<T> : IBaseService<T>
        where T : class
    {
        protected readonly AlienContext _context;

        public BaseService(AlienContext context)
        {
            _context = context ??
                throw new ArgumentNullException(nameof(context));
        }


        /// <summary>
        /// Create a generic Entity based on the type declared in the class declaration
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Create(T entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));
            _context.Entry(entity).State = EntityState.Added;
        }

        public virtual void Delete(T entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));
            _context.Entry(entity).State = EntityState.Deleted;
        }

        public virtual async ValueTask<IEnumerable<T>> GetEntitiesAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public virtual async ValueTask<T> GetEntityAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        public virtual void Update(T entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
