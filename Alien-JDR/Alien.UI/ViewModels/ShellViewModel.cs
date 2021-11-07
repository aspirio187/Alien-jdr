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
        private readonly IAuthenticator _authenticator;
        private readonly IDialogService _dialogService;

        private DelegateCommand _navigateCharacterCommand;
        public DelegateCommand NavigateCharacterCommand => _navigateCharacterCommand ??= new DelegateCommand(NavigateCharacter);
        public DelegateCommand NavigatePartiesCommand { get; set; }
        public DelegateCommand NavigateHistoryCommand { get; set; }
        public DelegateCommand NavigateNotificationCommand { get; set; }

        public ShellViewModel(IRegionManager regionManager, IAuthenticator authenticator, IDialogService dialogService, IRegionNavigationService regionNavigationService)
            : base(regionNavigationService)
        {
            _regionManager = regionManager ??
                throw new ArgumentNullException(nameof(regionManager));
            _authenticator = authenticator ??
                throw new ArgumentNullException(nameof(authenticator));
            _dialogService = dialogService ??
                throw new ArgumentNullException(nameof(dialogService));

            // Déclaration des commandes
            LoadCommand = new(Load);
        }

        protected override void Load()
        {
            // TODO : Check si le joueur a déjà une session active

            _dialogService.ShowDialog("LoginView", null);

            _regionNavigationService.Region = _regionManager.Regions[Global.REGION_NAME];
            Navigate(ViewsEnum.CharactersView);
        }

        public void NavigateCharacter()
        {
            Navigate(ViewsEnum.CharactersView);
        }
    }
}
