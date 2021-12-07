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
        public DateTime SendTime { get; set; }
        public Guid UserFromId { get; set; }
        public string SenderName { get; set; }
        public int PartyId { get; set; }
        public string PartyName { get; set; }
        public string PartyMode { get; set; }
        public bool IsAccepted { get; set; }
    }
}
