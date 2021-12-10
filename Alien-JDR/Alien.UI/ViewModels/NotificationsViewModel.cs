﻿using Alien.BLL.Interfaces;
using Alien.UI.Helpers;
using Alien.UI.Models;
using Alien.UI.States;
using AutoMapper;
using Prism.Commands;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.UI.ViewModels
{
    public class NotificationsViewModel : ViewModelBase
    {
        public readonly INotificationService _notificationService;

        private DelegateCommand<NotificationStatusEnum> _respondCommand;
        public override DelegateCommand LoadCommand => _loadCommand ??= new(async () => await LoadAsync());
        public DelegateCommand<NotificationStatusEnum> RespondCommand => _respondCommand ??= new DelegateCommand<NotificationStatusEnum>(Respond, CanRespond);

        public ObservableCollection<NotificationModel> Notifications { get; set; }

        public NotificationsViewModel(IRegionNavigationService regionNavigationService, IAuthenticator authenticator, IMapper mapper, INotificationService notificationService)
            : base(regionNavigationService, authenticator, mapper)
        {
            _notificationService = notificationService ??
                throw new ArgumentNullException(nameof(notificationService));

            RespondCommand.RaiseCanExecuteChanged();
        }

        protected override async Task LoadAsync()
        {
            var notifs = await _notificationService.GetUserNotifications(_authenticator.User.Id);
            List<NotificationModel> not = new List<NotificationModel>();
            foreach (var notif in notifs)
            {
                not.Add(new NotificationModel()
                {
                    PartyHost = notif.SenderName,
                    PartyName = notif.Lobby.Name,
                    Mode = notif.Lobby.Mode,
                    NotificationStatus = notif.IsAccepted ? NotificationStatusEnum.Accepted : NotificationStatusEnum.Pending,
                    SendAt = notif.SentTime,
                    HostId = notif.SenderId,
                    Id = notif.Id
                });
            }
            Notifications = new(not.OrderBy(n => n.Id));
        }

        public bool CanRespond(NotificationStatusEnum notificationStatusEnum)
        {
            return notificationStatusEnum == NotificationStatusEnum.Pending;
        }

        public void Respond(NotificationStatusEnum notificationStatusEnum)
        {
            switch (notificationStatusEnum)
            {
                case NotificationStatusEnum.Accepted:
                    break;
                case NotificationStatusEnum.Denied:
                    break;
            }

            RespondCommand.RaiseCanExecuteChanged();
        }
    }
}
