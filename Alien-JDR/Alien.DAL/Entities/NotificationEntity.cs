using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.DAL.Entities
{
    public enum NotificationStatucEnum
    {
        Accepted,
        Pending,
        Denied
    }

    public class NotificationEntity
    {
        public int Id { get; set; }
        public DateTime SentTime { get; set; }
        public Guid SenderId { get; set; }
        public UserEntity Sender { get; set; }
        public Guid ReceiverId { get; set; }
        public UserEntity Receiver { get; set; }
        public int LobbyId { get; set; }
        public LobbyEntity Lobby { get; set; }
        public NotificationStatucEnum Status { get; set; }
    }
}
