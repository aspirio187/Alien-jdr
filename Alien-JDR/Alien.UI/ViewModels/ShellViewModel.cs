using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.UI.ViewModels
{
    public class ShellViewModel : BindableBase
    {
        private readonly RegionManager _regionManager;
        private readonly RegionNavigationService _regionNavigationService;

        public ShellViewModel(RegionManager regionManager, RegionNavigationService regionNavigationService)
        {
            _regionManager = regionManager ?? throw new ArgumentNullException(nameof(regionManager));
            _regionNavigationService = regionNavigationService ?? throw new ArgumentNullException(nameof(regionNavigationService));
        }

        public void Load()
        {
            _regionNavigationService.Region = _regionManager.Regions["MainRegion"];
            _regionNavigationService.RequestNavigate(new Uri("CharacterHomeView", UriKind.Relative), null);
        }
    }
}
