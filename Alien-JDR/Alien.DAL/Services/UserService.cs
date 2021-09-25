using Alien.DAL.Entities;
using Alien.DAL.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.DAL.Services
{
    public class UserService<T> : BaseService<T>, IUserService<T> 
        where T : UserEntity
    {
        public UserService(AlienContext context)
            : base(context)
        {

        }
    }
}
