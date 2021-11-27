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
    public class TalentRepository : RepositoryBase<TalentEntity, int>, ITalentRepository
    {
        public TalentRepository(AlienContext context)
            : base(context)
        {

        }

        public async Task<IEnumerable<TalentEntity>> GetCharacterTalentsAsync(int characterId)
        {
            return await _context.Talents.Where(t => t.Characters.Any(c => c.Id == characterId)).ToListAsync();
        }
    }
}