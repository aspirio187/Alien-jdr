using Alien.DAL.Entities;
using Alien.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.DAL.Repositories
{
    public class UserRepository : RepositoryBase<UserEntity, Guid>, IUserRepository
    {
        public UserRepository(AlienContext context) : base(context) { }

        public async Task<UserEntity> SignInAsync(string username, string password)
        {
            if (username is null) throw new ArgumentNullException(nameof(username));
            if (password is null) throw new ArgumentNullException(nameof(password));
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
            return user;
        }
    }
}
