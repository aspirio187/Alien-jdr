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

        private ObservableCollection<NotificationModel> _notifications;

        public ObservableCollection<NotificationModel> Notifications
        {
            get { return _notifications; }
            set { SetProperty(ref _notifications, value); }
        }

        private DelegateCommand<object> _respondCommand;

        public DelegateCommand<object> RespondCommand => _respondCommand ??= new DelegateCommand<object>(Respond, CanRespond);

        private DelegateCommand _checkChangesCommand;

        public DelegateCommand CheckChangesCommand => _checkChangesCommand ??= new DelegateCommand(CheckChanges);

        public override DelegateCommand LoadCommand => _loadCommand ??= new(async () => await LoadAsync());


        public NotificationsViewModel(IRegionNavigationService regionNavigationService, IAuthenticator authenticator, IMapper mapper, INotificationService notificationService)
            : base(regionNavigationService, authenticator, mapper)
        {
            _notificationService = notificationService ??
                throw new ArgumentNullException(nameof(notificationService));
        }

        public void CheckChanges()
        {
            RespondCommand?.RaiseCanExecuteChanged();
        }

        public bool CanRespond(object row)
        {
            if (row is null) return false;
            object[] bindings = (row as object[]);
            NotificationStatusEnum status = (NotificationStatusEnum)bindings[1];
            return status.Equals(NotificationStatusEnum.Pending);
        }

        public void Respond(object row)
        {
            object[] bindings = (row as object[]);
            NotificationStatusEnum buttonStatus = (NotificationStatusEnum)bindings[0];
            NotificationStatusEnum notificationStatus = (NotificationStatusEnum)bindings[1];
            int id = (int)bindings[2];

            switch (buttonStatus)
            {
                case NotificationStatusEnum.Accepted:
                    // TODO : Change l'état dans la base de donnée et navigue vers le lobby
                    break;
                case NotificationStatusEnum.Denied:
                    // TODO : Change l'état dans la base de donnée
                    NotificationModel notificationToUpdate = Notifications.FirstOrDefault(n => n.Id == id);
                    notificationToUpdate.NotificationStatus = NotificationStatusEnum.Denied;
                    _notificationService.UpdateNotificationStatus(id, NotificationStatusEnum.Denied.ToString());
                    break;
            }

            RespondCommand.RaiseCanExecuteChanged();
        }

        protected override async Task LoadAsync()
        {
            IEnumerable<NotificationDto> notifs = await _notificationService.GetUserNotifications(_authenticator.User.Id);
            Notifications = new(_mapper.Map<IEnumerable<NotificationModel>>(notifs).OrderBy(n => n.SentAt));
        }
    }
}
