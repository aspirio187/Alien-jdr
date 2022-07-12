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
    public class LobbyRepository : RepositoryBase<LobbyEntity, int>, ILobbyRepository
    {
        public LobbyRepository(AlienContext context)
            : base(context)
        {

        }

        public async Task<IEnumerable<LobbyEntity>> GetAllLobbiesWithPlayersAsync()
        {
            List<LobbyEntity> lobbies = await _context.Lobbies.Include(l => l.PartyPlayers).ToListAsync();
            return lobbies;
        }
    }
}
