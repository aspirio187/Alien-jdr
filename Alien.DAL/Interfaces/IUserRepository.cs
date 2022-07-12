using Alien.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.DAL.Interfaces
{
    public interface IUserRepository : IRepositoryBase<UserEntity, Guid>
    {
        Task<UserEntity> SignInAsync(string username, string password);
        Task<bool> UserExists(Guid userId);
    }
}
