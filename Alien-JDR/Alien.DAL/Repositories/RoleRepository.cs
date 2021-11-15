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
    public class RoleRepository : RepositoryBase<RoleEntity, Guid>, IRoleRepository
    {
        public RoleRepository(AlienContext context)
            : base(context)
        {

        }

        public async Task<IEnumerable<RoleEntity>> GetUserRoleAsync(Guid userId)
        {
            if (userId == Guid.Empty) throw new ArgumentException($"User ID \"{userId}\" is empty!");
            if (!await _context.Users.AnyAsync(u => u.Id == userId)) throw new Exception($"The user with ID \"{userId}\" doesn't exist!");
            return (await _context.Users.FindAsync(userId)).Roles.ToList();
        }
    }
}