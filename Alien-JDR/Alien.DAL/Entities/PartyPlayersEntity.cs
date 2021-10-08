using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.DAL.Entities
{
    public class PartyPlayersEntity
    {
        public int Id { get; set; }

        public Guid UserId { get; set; }
        public UserEntity User { get; set; }

        public int PartyId { get; set; }
        public PartyEntity Party { get; set; }

        public int CharacterId { get; set; }
        public CharacterEntity Character { get; set; }
    }
}
