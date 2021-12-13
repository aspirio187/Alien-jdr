using Alien.BLL.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alien.BLL.Interfaces
{
    public interface ILobbyService
    {
        Task<LobbyDto> GetLobby(int lobbyId);
        Task<IEnumerable<LobbyDto>> GetLobbiesAsync();
        LobbyDto CreateLobby(CreateLobbyDto lobby);
        bool DeleteLobby(int lobbyId);
        bool UpdateLobby(LobbyDto lobby);
        bool PlayerCanJoin(int lobbyId, Guid userId);
        bool UpdateHostIp(int lobbyId, string hostIp);
    }
}
