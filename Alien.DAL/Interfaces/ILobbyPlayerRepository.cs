﻿using Alien.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.DAL.Interfaces
{
    public interface ILobbyPlayerRepository : IRepositoryBase<LobbyPlayerEntity, int>
    {
        Task<IEnumerable<LobbyPlayerEntity>> GetLobbyPlayersAsync(int lobbyId);
    }
}
