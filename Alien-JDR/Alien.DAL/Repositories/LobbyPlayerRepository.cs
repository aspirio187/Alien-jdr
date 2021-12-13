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
    public class LobbyPlayerRepository : RepositoryBase<LobbyPlayerEntity, int>, ILobbyPlayerRepository
    {
        public LobbyPlayerRepository(AlienContext context) : base(context)
        {
        }

        public async Task<IEnumerable<LobbyPlayerEntity>> GetLobbyPlayersAsync(int lobbyId)
        {
            return await _context.LobbyPlayers.Where(lb => lb.lobbyId == lobbyId).ToListAsync();
        }
    }
}
