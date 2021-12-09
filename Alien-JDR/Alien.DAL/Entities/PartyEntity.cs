using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.DAL.Entities
{
    public class PartyEntity
    {
        public int Id { get; set; }
        public string Mode { get; set; }
        public string Name { get; set; }
        public int MaximumPlayers { get; set; }
        public bool IsStarted { get; set; }
        public bool IsOver { get; set; }
        public string HostIp { get; set; }

        public ICollection<NotificationEntity> Notifications { get; set; }

        public ICollection<PartyPlayersEntity> PartyPlayers { get; set; }
        // TODO : ADD Partyplayers( many, required false ), creator ( unique et required ) 
    }
}
