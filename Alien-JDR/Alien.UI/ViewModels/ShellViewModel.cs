using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alien.UI.Views;
using Prism.Commands;
using Alien.UI.Helpers;
using Alien.UI.States;
using Prism.Services.Dialogs;
using TableDependency.SqlClient;
using Alien.BLL.Interfaces;
using Alien.BLL.Helpers;
using AutoMapper;

namespace Alien.UI.ViewModels
{
    public class ShellViewModel : ViewModelBase
    {
        private readonly INotificationService _notificationService;
        private readonly IRegionManager _regionManager;
        private readonly IDialogService _dialogService;

        private bool _notificationReceived;

        public bool NotificationReceived
        {
            get { return _notificationReceived; }
            set { SetProperty(ref _notificationReceived, value); }
        }

        private DelegateCommand _navigateCharacterCommand;
        private DelegateCommand _navigateManuelCommand;
        private DelegateCommand _navigateCreditCommand;
        private DelegateCommand _navigateLobbiesCommand;

        public DelegateCommand NavigateCharacterCommand => _navigateCharacterCommand ??= new DelegateCommand(NavigateCharacter);
        public DelegateCommand NavigateManuelCommand => _navigateManuelCommand ??= new DelegateCommand(NavigateManuel);
        public DelegateCommand NavigateCreditCommand => _navigateCreditCommand ??= new DelegateCommand(NavigateCredit);
        public DelegateCommand NavigateLobbiesCommand => _navigateLobbiesCommand ??= new DelegateCommand(NavigateLobbies);
        public override DelegateCommand LoadCommand => _loadCommand ??= new DelegateCommand(async () => await LoadAsync());

        public ShellViewModel(IRegionNavigationService regionNavigationService, IAuthenticator authenticator, IMapper mapper, IRegionManager regionManager,
            IDialogService dialogService, INotificationService notificationService)
            : base(regionNavigationService, authenticator, mapper)
        {
            _regionManager = regionManager ??
                throw new ArgumentNullException(nameof(regionManager));
            _dialogService = dialogService ??
                throw new ArgumentNullException(nameof(dialogService));
            _notificationService = notificationService ??
                throw new ArgumentNullException(nameof(notificationService));

            _notificationService.OnNotificationReceived += Notification_Received;
        }

        private void Notification_Received(object sender, NotificationEventArgs e)
        {
            NotificationReceived = true;
        }

        protected override async Task LoadAsync()
        {
            if (!await _authenticator.IsConnected())
            {
                _dialogService.ShowDialog("LoginView", null);
            }

            if (await _notificationService.CheckPendingNotifications(_authenticator.User.Id))
            {
                NotificationReceived = true;
            }

            _regionNavigationService.Region = _regionManager.Regions[Global.REGION_NAME];
            Navigate(ViewsEnum.CharactersView);
        }

        public void NavigateCharacter()
        {
            Navigate(ViewsEnum.CharactersView);
        }

        public void NavigateLobbies()
        {
            NotificationReceived = false;
            Navigate(ViewsEnum.LobbiesView);
        }

        public void NavigateManuel()
        {
            Navigate(ViewsEnum.ManuelView);
        }

        public void NavigateCredit()
        {
            Navigate(ViewsEnum.CreditView);
        }
    }
}
