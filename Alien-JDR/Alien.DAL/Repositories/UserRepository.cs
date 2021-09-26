using Alien.DAL.Entities;
using Alien.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.DAL.Repositories
{
    public class UserRepository : RepositoryBase<UserEntity, int>, IUserRepository
    {
        public UserRepository(AlienContext context) : base(context) { }

        public UserEntity Login(string email, string password)
        {
            throw new NotImplementedException();
        }

        public bool Register(string email, string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
