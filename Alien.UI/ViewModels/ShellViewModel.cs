using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alien.UI.Views;
using Alien.UI.Helpers;
using Alien.UI.States;
using TableDependency.SqlClient;
using Alien.BLL.Interfaces;
using Alien.BLL.Helpers;
using AutoMapper;
using System.Windows.Controls;
using System.Windows.Input;
using Alien.UI.Commands;
using System.Threading;
using Alien.UI.Managers;
using ViewBase = Alien.UI.Views.ViewBase;

namespace Alien.UI.ViewModels
{
    public class ShellViewModel : ViewModelBase
    {
        private readonly INotificationService _notificationService;
        private readonly NavigationManager _navigationManager;

        private ViewBase? _currentView;

        public ViewBase? CurrentView
        {
            get { return _currentView; }
            set
            {
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                _currentView = value;
                NotifyPropertyChanged();
            }
        }


        private bool _notificationReceived;

        public bool NotificationReceived
        {
            get { return _notificationReceived; }
            set
            {
                _notificationReceived = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand NavigateCharacterCommand { get; private set; }
        public ICommand NavigateLobbiesCommand { get; private set; }
        public ICommand NavigateNotificationsCommand { get; private set; }
        public ICommand NavigateManuelCommand { get; private set; }
        public ICommand NavigateCreditCommand { get; private set; }

        public ShellViewModel(IAuthenticator authenticator, IMapper mapper, INotificationService notificationService,
            NavigationManager navigationManager)
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

            _notificationService.OnNotificationReceived += Notification_Received;

            _navigationManager = navigationManager;

            _navigationManager.OnCurrentViewChanged += OnCurrentViewChanged;

            // Commands
            NavigateCharacterCommand = new RelayCommand(NavigateCharacter);
            NavigateLobbiesCommand = new RelayCommand(NavigateLobbies);
            NavigateNotificationsCommand = new RelayCommand(NavigateNotifications);
            NavigateManuelCommand = new RelayCommand(NavigateManuel);
            NavigateCreditCommand = new RelayCommand(NavigateCredit);

            NavigateCharacterCommand.Execute(this);
        }

        private void OnCurrentViewChanged(ContentControl? currentView)
        {
            if (_navigationManager is null)
            {
                throw new NullReferenceException(nameof(_navigationManager));
            }

            if (currentView is null)
            {
                throw new ArgumentNullException(nameof(currentView));
            }

            CurrentView = currentView as ViewBase;
        }

        private void Notification_Received(object? sender, NotificationEventArgs e)
        {
            if (e.Notification.ReceiverId == _authenticator.User.Id)
            {
                NotificationReceived = true;
            }
        }

        public override void OnInit()
        {
            base.OnInit();

            Task.Run(async () =>
            {
                if (!await _authenticator.IsConnected())
                {
                    _navigationManager.OpenDialog("LoginView", this);
                }

                if (await _notificationService.CheckPendingNotifications(_authenticator.User.Id))
                {
                    NotificationReceived = true;
                }

                //_regionNavigationService.Region = _regionManager.Regions[Global.REGION_NAME];
                NavigateCharacterCommand.Execute(this);
            });
        }

        public void NavigateCharacter()
        {
            _navigationManager.Navigate(nameof(CharactersView));
        }

        public void NavigateLobbies()
        {
            _navigationManager.Navigate(nameof(LobbiesView));
        }

        public void NavigateNotifications()
        {
            NotificationReceived = false;
            _navigationManager.Navigate(nameof(NotificationsView));
        }

        public void NavigateManuel()
        {
            _navigationManager.Navigate(nameof(ManuelView));
        }

        public void NavigateCredit()
        {
            _navigationManager.Navigate(nameof(CreditView));
        }
    }
}