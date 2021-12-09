using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.DAL.Entities
{
    public class NotificationEntity
    {
        public int Id { get; set; }
        public DateTimeOffset SentTime { get; set; }
        public Guid? SenderId { get; set; }
        public UserEntity Sender { get; set; }
        public Guid? ReceiverId { get; set; }
        public UserEntity Receiver { get; set; }
        public int LobbyId { get; set; }
        public LobbyEntity Lobby { get; set; }
        public bool IsAccepted { get; set; }
    }
}
