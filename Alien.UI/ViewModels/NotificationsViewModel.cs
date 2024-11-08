using Alien.BLL.Dtos;
using Alien.BLL.Interfaces;
using Alien.UI.Commands;
using Alien.UI.Helpers;
using Alien.UI.Managers;
using Alien.UI.Models;
using Alien.UI.States;
using Alien.UI.Views;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Alien.UI.ViewModels
{
    public class NotificationsViewModel : ViewModelBase
    {
        private readonly INotificationService _notificationService;
        private readonly NavigationManager _navigationManager;

        private ObservableCollection<NotificationModel> _notifications = new();

        public ObservableCollection<NotificationModel> Notifications
        {
            get { return _notifications; }
            set
            {
                _notifications = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand RespondCommand { get; private set; }
        public ICommand CheckChangesCommand { get; private set; }
        public ICommand LoadCommand { get; private set; }


        public NotificationsViewModel(IAuthenticator authenticator, IMapper mapper,
            INotificationService notificationService, NavigationManager navigationManager)
            : base(authenticator, mapper)
        {
            if (notificationService is null)
            {
                throw new ArgumentNullException(nameof(notificationService));
            }

            if (navigationManager is null)
            {
                throw new ArgumentNullException(nameof(navigationManager));
            }

            _notificationService = notificationService;
            _navigationManager = navigationManager;

            RespondCommand = new RelayCommand<object>(Respond, CanRespond);
            CheckChangesCommand = new RelayCommand(CheckChanges);
            LoadCommand = new RelayCommand(async () => await LoadAsync());
        }

        public void CheckChanges()
        {
            // TODO : Check for new notifications
        }

        public bool CanRespond(object row)
        {
            if (row is null) return false;
            object[] bindings = (row as object[]);
            NotificationStatusEnum status = (NotificationStatusEnum)bindings[1];
            return status.Equals(NotificationStatusEnum.Pending);
        }

        public async void Respond(object row)
        {
            object[] bindings = (row as object[]);
            NotificationStatusEnum buttonStatus = (NotificationStatusEnum)bindings[0];
            NotificationStatusEnum notificationStatus = (NotificationStatusEnum)bindings[1];
            int id = (int)bindings[2];

            switch (buttonStatus)
            {
                case NotificationStatusEnum.Accepted:
                    Dictionary<string, object> parameters = new Dictionary<string, object>()
                    {
                        { Global.LOBBY_ID, id }
                    };
                    _navigationManager.Navigate(nameof(LobbyCreationView), parameters: parameters);
                    // TODO : Change l'état dans la base de donnée et navigue vers le lobby
                    break;
                case NotificationStatusEnum.Denied:
                    // TODO : Change l'état dans la base de donnée
                    NotificationModel notificationToUpdate = Notifications.FirstOrDefault(n => n.Id == id);
                    if (await _notificationService.UpdateNotificationStatus(id,
                            NotificationStatusEnum.Denied.ToString()))
                    {
                        notificationToUpdate.NotificationStatus = NotificationStatusEnum.Denied;
                    }

                    break;
            }
        }

        public async Task LoadAsync()
        {
            IEnumerable<NotificationDto> notifs =
                await _notificationService.GetUserNotifications(_authenticator.User.Id);
            Notifications = new(_mapper.Map<IEnumerable<NotificationModel>>(notifs).OrderBy(n => n.SentAt));
        }
    }
}