using Alien.UI.States;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.UI.ViewModels
{
    public class CharacterInfosCreationViewModel : ViewModelBase, IJournalAware
    {
        public CharacterInfosCreationViewModel(IRegionNavigationService regionNavigationService, IAuthenticator authenticator)
            : base(regionNavigationService, authenticator)
        {

        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
        }

        public bool PersistInHistory()
        {
            return true;
        }
    }
}