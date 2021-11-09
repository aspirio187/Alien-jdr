using Prism.Mvvm;
using Prism.Regions;
using System;
using Alien.UI.Core;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alien.UI.Views;
using Prism.Commands;
using Alien.UI.Helpers;
using Alien.UI.States;
using Prism.Services.Dialogs;

namespace Alien.UI.ViewModels
{
    public class ShellViewModel : ViewModelBase
    {
        private readonly IRegionManager _regionManager;
        private readonly IDialogService _dialogService;

        private DelegateCommand _navigateCharacterCommand;
        public DelegateCommand NavigateCharacterCommand => _navigateCharacterCommand ??= new DelegateCommand(NavigateCharacter);
        public DelegateCommand NavigatePartiesCommand { get; set; }
        public DelegateCommand NavigateHistoryCommand { get; set; }
        public DelegateCommand NavigateNotificationCommand { get; set; }
        public override DelegateCommand LoadCommand => _loadCommand ??= new DelegateCommand(async () => await LoadAsync());

        public ShellViewModel(IRegionManager regionManager, IDialogService dialogService, IRegionNavigationService regionNavigationService, IAuthenticator authenticator)
            : base(regionNavigationService, authenticator)
        {
            _regionManager = regionManager ??
                throw new ArgumentNullException(nameof(regionManager));
            _dialogService = dialogService ??
                throw new ArgumentNullException(nameof(dialogService));
        }

        protected override async Task LoadAsync()
        {
            if(!await _authenticator.IsConnected())
            {
                _dialogService.ShowDialog("LoginView", null);
            }

            _regionNavigationService.Region = _regionManager.Regions[Global.REGION_NAME];
            Navigate(ViewsEnum.CharactersView);
        }

        public void NavigateCharacter()
        {
            Navigate(ViewsEnum.CharactersView);
        }
    }
}
