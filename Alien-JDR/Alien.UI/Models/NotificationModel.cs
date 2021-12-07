using Alien.UI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.UI.Models
{
    public class NotificationModel : ModelBase
    {
        public int Id { get; set; }
        public Guid HostId { get; set; }
        public string PartyName { get; set; }
        public string Mode { get; set; }
        public string PartyHost { get; set; }
        public DateTime SendAt { get; set; }
        public NotificationStatusEnum NotificationStatus { get; set; }
    }
}
