using Alien.BLL.Interfaces;
using Alien.UI.Helpers;
using Alien.UI.Models;
using Alien.UI.States;
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

        public override DelegateCommand LoadCommand => _loadCommand ??= new(async () => await LoadAsync());

        private DelegateCommand<NotificationStatusEnum> _respondCommand;

        public DelegateCommand<NotificationStatusEnum> RespondCommand => _respondCommand ??= new DelegateCommand<NotificationStatusEnum>(Respond);

        public ObservableCollection<NotificaitonModel> Notifications { get; set; }

        public NotificationsViewModel(IRegionNavigationService regionNavigationService, IAuthenticator authenticator, INotificationService notificationService)
            : base(regionNavigationService, authenticator)
        {
            _notificationService = notificationService ??
                throw new ArgumentNullException(nameof(notificationService));
        }

        protected override async Task LoadAsync()
        {
            // TODO : Charger toutes les notifs depuis la DB
        }

        public void Respond(NotificationStatusEnum notificationStatusEnum)
        {
            switch (notificationStatusEnum)
            {
                case NotificationStatusEnum.Accepted:
                    break;
                case NotificationStatusEnum.Denied:
                    break;
                default:
                    break;
            }
        }
    }
}
