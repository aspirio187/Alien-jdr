using Alien.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.DAL.Repositories
{
    public abstract class RepositoryBase<TEntity, TKey> : IRepositoryBase<TEntity, TKey>
        where TEntity : class
    {
        protected readonly AlienContext _context;

        public RepositoryBase(AlienContext context)
        {
            _context = context ??
                throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Return the complete list of entities asynchronously
        /// </summary>
        /// <returns>IEnumerable <typeparamref name="TEntity"/> </returns>
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        /// <summary>
        /// Get an entity from the database by its ID asynchronously
        /// </summary>
        /// <param name="key">ID of the entity</param>
        /// <returns><typeparamref name="TEntity"/></returns>
        public virtual async Task<TEntity> GetByKeyAsync(TKey key)
        {
            TEntity entity = await _context.Set<TEntity>().FindAsync(key);
            _context.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        /// <summary>
        /// Create an entity in the database
        /// </summary>
        /// <param name="entity"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public virtual TEntity Create(TEntity entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));
            TEntity saved = _context.Add(entity).Entity;
            _context.Entry(entity).State = EntityState.Detached;
            return saved;
        }

        /// <summary>
        /// Update the entity from the database
        /// </summary>
        /// <param name="entity"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public virtual void Update(TEntity entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));
            EntityEntry entry = _context.Entry(entity);
            entry.State = EntityState.Modified;
        }

        /// <summary>
        /// Delete the entity from the database
        /// </summary>
        /// <param name="entity"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public virtual void Delete(TEntity entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));
            _context.Remove(entity);
        }

        /// <summary>
        /// Save changes from the context to the database
        /// </summary>
        /// <returns>True if one or more changes were sent to the database. false Otherwise</returns>
        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
