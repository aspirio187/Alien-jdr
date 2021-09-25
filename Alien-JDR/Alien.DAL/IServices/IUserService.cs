using Alien.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.DAL.IServices
{
    public interface IUserService<T> : IBaseService<T>
        where T : UserEntity
    {
        
    }
}
