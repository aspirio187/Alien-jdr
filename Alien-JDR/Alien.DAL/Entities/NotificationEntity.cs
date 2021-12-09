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
        public DateTimeOffset SendTime { get; set; }
        public Guid? UserFromId { get; set; }
        public UserEntity UserFrom { get; set; }
        public Guid? UserToId { get; set; }
        public UserEntity UserTo { get; set; }
        public int? PartyId { get; set; }
        public LobbyEntity Party { get; set; }
        public bool IsAccepted { get; set; }
    }
}
