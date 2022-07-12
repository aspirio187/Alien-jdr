using Alien.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableDependency.SqlClient.Base.Delegates;

namespace Alien.DAL.Interfaces
{
    public interface INotificationRepository : IRepositoryBase<NotificationEntity, int>
    {
        event ChangedEventHandler<NotificationEntity> NotificationEvent;
        Task<bool> UserHasPendingNotification(Guid userId);
        Task<IEnumerable<NotificationEntity>> GetNotificationsAsync();
    }
}
