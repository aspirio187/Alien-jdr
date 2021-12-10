using Alien.BLL.Interfaces;
using Alien.UI.Helpers;
using Alien.UI.Models;
using Alien.UI.States;
using AutoMapper;
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
        private readonly ICharacterService _characterService;
        private readonly IUserService _userService;
        private readonly INotificationService _notificationService;

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
        public ObservableCollection<LobbyUserModel> AvailableUsers { get; set; }

        private LobbyUserModel _selectedUser;

        public LobbyUserModel SelectedUser
        {
            get { return _selectedUser; }
            set { SetProperty(ref _selectedUser, value); }
        }

        public ObservableCollection<LobbyCharacterModel> AvailableCharacters { get; set; }

        private LobbyCharacterModel _selectedNpcCharacter;

        public LobbyCharacterModel SelectedNpcCharacter
        {
            get { return _selectedNpcCharacter; }
            set { SetProperty(ref _selectedNpcCharacter, value); }
        }

        private DelegateCommand _loadPlayersCommand;
        private DelegateCommand _invitePlayerCommand;
        private DelegateCommand _startGameCommand;
        private DelegateCommand _loadCharactersCommand;
        private DelegateCommand _addNpcCharacterCommand;

        public DelegateCommand LoadPlayersCommand => _loadPlayersCommand ??= new DelegateCommand(LoadPlayers);
        public DelegateCommand InvitePlayerCommand => _invitePlayerCommand ??= new DelegateCommand(InvitePlayer);
        public DelegateCommand LoadCharactersCommand => _loadCharactersCommand ??= new DelegateCommand(LoadCharacters);
        public DelegateCommand AddNpcCharacterCommand => _addNpcCharacterCommand ??= new DelegateCommand(AddNpcCharacter);
        public DelegateCommand StartGameCommand => _startGameCommand ??= new DelegateCommand(StartGame);


        public LobbyCreationViewModel(IRegionNavigationService regionNavigationService, IAuthenticator authenticator, IMapper mapper, ILobbyService lobbyService,
            ICharacterService characterService, IUserService userService, INotificationService notificationService)
            : base(regionNavigationService, authenticator, mapper)
        {
            _lobbyService = lobbyService ??
                throw new ArgumentNullException(nameof(lobbyService));
            _characterService = characterService ??
                throw new ArgumentNullException(nameof(characterService));
            _userService = userService ??
                throw new ArgumentNullException(nameof(userService));
            _notificationService = notificationService ??
                throw new ArgumentNullException(nameof(notificationService));
        }

        public async void LoadPlayers()
        {
            List<LobbyUserModel> users = _mapper.Map<List<LobbyUserModel>>(await _userService.GetUsersAsync());

            users.Remove(users.FirstOrDefault(u => u.Id == _authenticator.User.Id));

            foreach (var player in LobbyPlayers)
            {
                users.Remove(users.FirstOrDefault(u => u.Id == player.UserId));
            }

            AvailableUsers = new(users);
        }

        public void InvitePlayer()
        {
            if (SelectedUser is not null && SelectedUser.IsValid)
            {
                AvailableUsers.Clear();
            }
            // TODO : Send notification to user
        }

        public async void LoadCharacters()
        {
            // TODO : Get all available characters
        }

        public void AddNpcCharacter()
        {
            if (SelectedNpcCharacter is not null && SelectedNpcCharacter.IsValid)
            {

            }
            // TODO : add npc character in the characters list
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
