using Alien.BLL.Interfaces;
using Alien.UI.States;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.UI.ViewModels
{
    public class LobbyCreationViewModel : ViewModelBase, IJournalAware
    {
        private readonly ILobbyService _lobbyService;

        public LobbyCreationViewModel(IRegionNavigationService regionNavigationService, IAuthenticator authenticator) 
            : base(regionNavigationService, authenticator)
        {

        }

        public bool PersistInHistory()
        {
            return true;
        }
    }
}
