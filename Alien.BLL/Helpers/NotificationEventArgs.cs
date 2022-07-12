using Alien.BLL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.BLL.Helpers
{
    public class NotificationEventArgs : EventArgs
    {
        public NotificationDto Notification { get; set; }

        public NotificationEventArgs()
        {

        }
    }
}
