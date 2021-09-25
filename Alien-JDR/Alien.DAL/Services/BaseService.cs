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
        /// Create an entity in the database
        /// </summary>
        /// <param name="entity"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public virtual void Create(T entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));
            _context.Entry(entity).State = EntityState.Added;
        }

        /// <summary>
        /// Delete the entity from the database
        /// </summary>
        /// <param name="entity"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public virtual void Delete(T entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));
            _context.Entry(entity).State = EntityState.Deleted;
        }

        /// <summary>
        /// Return the complete list of entities asynchronously
        /// </summary>
        /// <returns>IEnumerable <typeparamref name="T"/> </returns>
        public virtual async ValueTask<IEnumerable<T>> GetEntitiesAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        /// <summary>
        /// Get an entity from the database by its ID asynchronously
        /// </summary>
        /// <param name="id">ID of the entity</param>
        /// <returns><typeparamref name="T"/></returns>
        public virtual async ValueTask<T> GetEntityAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        /// <summary>
        /// Save changes from the context to the database
        /// </summary>
        /// <returns>True if one or more changes were sent to the database. false Otherwise</returns>
        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        /// <summary>
        /// Update the entity from the database
        /// </summary>
        /// <param name="entity"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public virtual void Update(T entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
