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
    public class WoundRepository : RepositoryBase<WoundEntity, int>, IWoundRepository
    {
        public WoundRepository(AlienContext context)
            : base(context)
        {

        }

        public async Task<IEnumerable<WoundEntity>> GetUserWoundsAsync(int characterId)
        {
            return await _context.Wounds.Where(t => t.CharacterId == characterId).ToListAsync();
        }
    }
}