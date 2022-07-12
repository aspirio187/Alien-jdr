using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.BLL.Dtos
{
    public class CreateNotificationDto
    {
        public Guid ReceiverId { get; set; }
        public Guid SenderId { get; set; }
        public int LobbyId { get; set; }
    }
}
