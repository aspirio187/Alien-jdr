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

namespace Alien.UI.ViewModels
{
    public class ShellViewModel : ViewModelBase
    {
        private readonly IRegionManager _regionManager;
        private readonly IAuthenticator _authenticator;

        public DelegateCommand NavigateCharacterCommand { get; set; }
        public DelegateCommand NavigatePartiesCommand { get; set; }
        public DelegateCommand NavigateHistoryCommand { get; set; }
        public DelegateCommand NavigateNotificationCommand { get; set; }

        public ShellViewModel(IRegionManager regionManager, IRegionNavigationService regionNavigationService, IAuthenticator authenticator)
            : base(regionNavigationService)
        {
            _regionManager = regionManager ??
                throw new ArgumentNullException(nameof(regionManager));

            // Déclaration des commandes
            LoadCommand = new(Load);
            NavigateCharacterCommand = new(NavigateCharacter);
            _authenticator = authenticator;
        }

        protected override void Load()
        {
            _regionNavigationService.Region = _regionManager.Regions[Global.REGION_NAME];
            Navigate(ViewsEnum.CharactersView);
        }

        public void NavigateCharacter()
        {
            Navigate(ViewsEnum.CharactersView);
        }
    }
}
