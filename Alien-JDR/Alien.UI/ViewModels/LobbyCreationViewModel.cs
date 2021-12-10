using Alien.BLL.Interfaces;
using Alien.UI.Helpers;
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
    public class LobbyCreationViewModel : ViewModelBase, IJournalAware
    {
        private readonly ILobbyService _lobbyService;

        private bool _isCreator;

        public bool IsCreator
        {
            get { return _isCreator; }
            set { SetProperty(ref _isCreator, value); }
        }


        private DelegateCommand _startGameCommand;

        public DelegateCommand StartGameCommand => _startGameCommand ??= new DelegateCommand(StartGame);

        public LobbyCreationViewModel(IRegionNavigationService regionNavigationService, IAuthenticator authenticator)
            : base(regionNavigationService, authenticator)
        {

        }

        public void StartGame()
        {

        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            if (_lobbyService.Create(lobbydto))
            {

            }
            else
            {
                Navigate(ViewsEnum.LobbiesView);
            }

            // TODO : Créer un lobby dans la base de donnée
            // TODO : Vérifie si l'utilisateur est le créateur du lobby
        }

        public bool PersistInHistory()
        {
            return true;
        }
    }
}
