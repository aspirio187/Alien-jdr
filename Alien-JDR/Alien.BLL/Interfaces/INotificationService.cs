using Alien.BLL.Dtos;
using Alien.BLL.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.BLL.Interfaces
{
    public interface INotificationService
    {
        event EventHandler<NotificationEventArgs> OnNotificationReceived;
        Task<IEnumerable<NotificationDto>> GetUserNotifications(Guid userId);
        Task<bool> CheckPendingNotifications(Guid userId);
    }
}
