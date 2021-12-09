using Alien.BLL.Dtos;
using Alien.BLL.Helpers;
using Alien.BLL.Interfaces;
using Alien.DAL.Entities;
using Alien.DAL.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableDependency.SqlClient.Base.Delegates;
using TableDependency.SqlClient.Base.EventArgs;

namespace Alien.BLL.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly ILobbyRepository _lobbyRepository;
        private readonly IMapper _mapper;

        public event EventHandler<NotificationEventArgs> OnNotificationReceived;

        public NotificationService(INotificationRepository notificationRepository, IMapper mapper, ILobbyRepository lobbyRepository)
        {
            _notificationRepository = notificationRepository ??
                throw new ArgumentNullException(nameof(notificationRepository));
            _lobbyRepository = lobbyRepository ??
                throw new ArgumentNullException(nameof(lobbyRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));

            _notificationRepository.NotificationEvent += Notification_Received;
        }

        private void Notification_Received(object sender, RecordChangedEventArgs<NotificationEntity> e)
        {
            OnNotificationReceived?.Invoke(this, new NotificationEventArgs()
            {
                Notification = _mapper.Map<NotificationDto>(e.Entity)
            });
        }

        public async Task<IEnumerable<NotificationDto>> GetUserNotifications(Guid userId)
        {
            IEnumerable<NotificationEntity> notifications = await _notificationRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<NotificationDto>>(notifications.Where(n => n.ReceiverId == userId));
        }
    }
}
