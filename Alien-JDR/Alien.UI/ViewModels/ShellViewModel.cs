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

namespace Alien.UI.ViewModels
{
    public class ShellViewModel : ViewModelBase
    {
        private readonly RegionManager _regionManager;

        public DelegateCommand NavigateCharacterCommand { get; set; }
        public DelegateCommand NavigatePartiesCommand { get; set; }
        public DelegateCommand NavigateHistoryCommand { get; set; }
        public DelegateCommand NavigateNotificationCommand { get; set; }

        public ShellViewModel(RegionManager regionManager, IRegionNavigationService regionNavigationService)
            : base(regionNavigationService)
        {
            _regionManager = regionManager ?? 
                throw new ArgumentNullException(nameof(regionManager));
        }

        protected override void LoadAsync()
        {
            _regionNavigationService.Region = _regionManager.Regions[Global.REGION_NAME];
            Navigate(ViewsEnum.CharactersView);
        }
    }
}
