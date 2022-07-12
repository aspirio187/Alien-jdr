using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.BLL.Dtos
{
    public class CreateLobbyPlayerDto
    {
        public Guid UserId { get; set; }
        public int LobbyId { get; set; }
        public int? CharacterId { get; set; }
        public bool IsCreator { get; set; }
    }
}
