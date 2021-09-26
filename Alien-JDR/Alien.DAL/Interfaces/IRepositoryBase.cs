﻿using System;
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
        Task<TEntity> GetByKey(TKey key);
        TEntity Create(TEntity entity);
        bool Update(TEntity entity);
        bool Delete(TKey key);
        bool SaveChanges();
    }
}
