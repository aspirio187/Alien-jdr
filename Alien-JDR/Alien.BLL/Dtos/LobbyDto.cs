using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.BLL.Dtos
{
    public class LobbyDto
    {
        public int Id { get; set; }
        public string Mode { get; set; }
        public string Name { get; set; }
        public string Creator { get; set; }
        public int MaximumPlayers { get; set; }
        public string Status { get; set; }
    }
}
