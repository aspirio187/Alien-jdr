using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.UI.Models
{
    public class LobbyPlayerArrival
    {
        public Guid UserId { get; set; }
        public string PlayerName { get; set; }
        public int? CharacterId { get; set; }
        public string CharacterName { get; set; }
        public string Status { get; set; }
        public string PlayerType { get; set; }
    }
}
