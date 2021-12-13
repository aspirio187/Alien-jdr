using Alien.BLL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.BLL.Interfaces
{
    public interface ILobbyPlayerService
    {
        bool CreateLobbyCreator(CreateLobbyPlayerDto lobbyPlayer);
        bool CreateLobbyPlayer(CreateLobbyPlayerDto lobbyPlayer);
        Task<bool> IsUserCreator(Guid userId, int lobbyId);
        Task<LobbyPlayerDto> GetLobbyPlayerAsync(Guid userId, int lobbyId);
    }
}
