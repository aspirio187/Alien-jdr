using Alien.UI.States;
using Prism.Commands;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.UI.ViewModels
{
    public class LobbiesViewModel : ViewModelBase
    {
        public override DelegateCommand LoadCommand => _loadCommand ??= new DelegateCommand(async () => await LoadAsync());

        public LobbiesViewModel(IRegionNavigationService regionNavigationService, IAuthenticator authenticator)
            : base(regionNavigationService, authenticator)
        {

        }

        protected override async Task LoadAsync()
        {
            // TODO : Charger toutes les parties
        }
    }
}
