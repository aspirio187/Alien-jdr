using Alien.BLL.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alien.BLL.Interfaces
{
    public interface ILobbyService
    {
        Task<IEnumerable<LobbyDto>> GetLobbiesAsync();
        LobbyDto CreateLobby(CreateLobbyDto lobby);
    }
}
