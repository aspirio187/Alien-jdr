using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.BLL.Dtos
{
    public class CreateLobbyDto
    {
        public string Mode { get; set; }
        public string Name { get; set; }
        public int MaximumPlayers { get; set; }
        public string HostIp { get; set; }
    }
}
