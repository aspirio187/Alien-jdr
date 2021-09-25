using Alien.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.DAL.IServices
{
    public interface IUserService
    {
        Task<UserEntity> GetUserAsync();
        Task<IEnumerable<UserEntity>> GetUsersAsync();
        void CreateUser(UserEntity user);
        void UpdateUSer(UserEntity user);
        void DeleteUser(UserEntity user);
    }
}
