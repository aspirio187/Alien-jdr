using Alien.BLL.Dtos;
using Alien.BLL.Interfaces;
using Alien.UI.Helpers;
using Alien.UI.Models;
using Alien.UI.States;
using AutoMapper;
using Prism.Commands;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.UI.ViewModels
{
    public class NotificationsViewModel : ViewModelBase
    {
        public readonly INotificationService _notificationService;

        public ObservableCollection<NotificationModel> Notifications { get; set; }

        private DelegateCommand<object> _respondCommand;

        public DelegateCommand<object> RespondCommand => _respondCommand ??= new DelegateCommand<object>(Respond, CanRespond);
        public override DelegateCommand LoadCommand => _loadCommand ??= new(async () => await LoadAsync());

        public NotificationsViewModel(IRegionNavigationService regionNavigationService, IAuthenticator authenticator, IMapper mapper, INotificationService notificationService)
            : base(regionNavigationService, authenticator, mapper)
        {
            _notificationService = notificationService ??
                throw new ArgumentNullException(nameof(notificationService));

            RespondCommand.RaiseCanExecuteChanged();
        }

        protected override async Task LoadAsync()
        {
            IEnumerable<NotificationDto> notifs = await _notificationService.GetUserNotifications(_authenticator.User.Id);

            // OLD WAY WITHOUT PROFILE //
            //List<NotificationModel> not = new List<NotificationModel>();
            //foreach (var notif in notifs)
            //{
            //    not.Add(new NotificationModel()
            //    {
            //        PartyHost = notif.SenderName,
            //        PartyName = notif.Lobby.Name,
            //        Mode = notif.Lobby.Mode,
            //        NotificationStatus = notif.Status.Equals("Accepted") ? NotificationStatusEnum.Accepted : notif.Status.Equals("Pending") ? NotificationStatusEnum.Pending : NotificationStatusEnum.Denied,
            //        SendAt = notif.SentTime,
            //        HostId = notif.SenderId,
            //        Id = notif.Id
            //    });
            //}
            //Notifications = new(not.OrderBy(n => n.SendAt));

            // NEW WAY WITH PROFILE //
            Notifications = new(_mapper.Map<IEnumerable<NotificationModel>>(notifs).OrderBy(n => n.SendAt));
        }

        public bool CanRespond(object notificationStatusEnum)
        {
            return true;
        }

        public void Respond(object notificationStatusEnum)
        {
            //switch (notificationStatusEnum)
            //{
            //    case NotificationStatusEnum.Accepted:

            //        Dictionary<string, object> parameters = new Dictionary<string, object>()
            //        {
            //            { Global.LOBBY_ID, null }
            //        };

            //        break;
            //    case NotificationStatusEnum.Denied:
            //        break;
            //}

            Debug.WriteLine(notificationStatusEnum);

            RespondCommand.RaiseCanExecuteChanged();
        }
    }
}
