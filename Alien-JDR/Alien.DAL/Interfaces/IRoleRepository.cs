using Alien.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.DAL.Interfaces
{
    public interface IRoleRepository : IRepositoryBase<RoleEntity, Guid>
    {
        Task<IEnumerable<RoleEntity>> GetUserRoleAsync(Guid userId);
    }
}
