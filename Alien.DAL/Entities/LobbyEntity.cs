﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.DAL.Entities
{
    public enum LobbyStatusEnum
    {
        Started,
        Waiting,
        Over
    }

    public class LobbyEntity
    {
        public int Id { get; set; }
        public string Mode { get; set; }
        public string Name { get; set; }
        public int MaximumPlayers { get; set; }
        public LobbyStatusEnum Status { get; set; }
        public string HostIp { get; set; }

        public ICollection<NotificationEntity> Notifications { get; set; }

        public ICollection<LobbyPlayerEntity> PartyPlayers { get; set; }
    }
}
