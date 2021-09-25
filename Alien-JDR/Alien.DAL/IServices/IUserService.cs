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
        Task<bool> SignInAsync(string email, string password);
        Task<bool> SignUpAsync();
        Task<UserEntity> GetUserAsync();
        Task<IEnumerable<UserEntity>> GetUsersAsync();
    }
}
