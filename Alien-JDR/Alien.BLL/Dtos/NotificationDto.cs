using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.BLL.Dtos
{
    public class NotificationDto
    {
        public int Id { get; set; }
        public DateTime SentTime { get; set; }
        public Guid SenderId { get; set; }
        public string SenderName { get; set; }
        public int LobbyId { get; set; }
        public string LobbyName { get; set; }
        public string LobbyMode { get; set; }
        public bool IsAccepted { get; set; }
    }
}
