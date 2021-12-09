using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.BLL.Dtos
{
    public class LobbyPlayerDto
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public UserDto User { get; set; }
        public int? CharacterId { get; set; }
        public CharacterLobbyDto Character { get; set; }
        public bool IsCreator { get; set; }
    }
}
