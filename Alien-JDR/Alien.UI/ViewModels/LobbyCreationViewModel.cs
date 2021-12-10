using Alien.BLL.Interfaces;
using Alien.UI.Helpers;
using Alien.UI.Models;
using Alien.UI.States;
using Prism.Commands;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private LobbyModeEnum _selectedGameMode;

        public LobbyModeEnum SelectedGameMode
        {
            get { return _selectedGameMode; }
            set { SetProperty(ref _selectedGameMode, value); }
        }

        public ObservableCollection<LobbyPlayerModel> LobbyPlayers { get; set; }
        public ObservableCollection<LobbyCharacterModel> AvailableCharacters { get; set; }
        public ObservableCollection<LobbyUserModel> AvailableUsers { get; set; }

        private DelegateCommand _startGameCommand;

        public DelegateCommand StartGameCommand => _startGameCommand ??= new DelegateCommand(StartGame);

        public LobbyCreationViewModel(IRegionNavigationService regionNavigationService, IAuthenticator authenticator, ILobbyService lobbyService)
            : base(regionNavigationService, authenticator)
        {
            _lobbyService = lobbyService ??
                throw new ArgumentNullException(nameof(lobbyService));
        }

        public void StartGame()
        {

        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            // TODO : Créer un lobby dans la base de donnée
            // TODO : Vérifie si l'utilisateur est le créateur du lobby
        }

        public bool PersistInHistory()
        {
            return true;
        }
    }
}
