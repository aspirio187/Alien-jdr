using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.DAL.Entities
{
    public class LobbyPlayerEntity
    {
        public int Id { get; set; }

        public Guid UserId { get; set; }
        public UserEntity User { get; set; }

        public int lobbyId { get; set; }
        public LobbyEntity Lobby { get; set; }

        public int? CharacterId { get; set; }
        public CharacterEntity Character { get; set; }

        public bool IsCreator { get; set; }
    }
}
