using Alien.BLL.Dtos;
using Alien.BLL.Interfaces;
using Alien.UI.Helpers;
using Alien.UI.States;
using AutoMapper;
using Prism.Commands;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.UI.ViewModels
{
    public class LobbiesViewModel : ViewModelBase
    {
        private readonly ILobbyService _lobbyService;
        private readonly IUserService _userService;

        //public ObservableCollection<LobbyDto> Lobbies { get; set; }
        private ObservableCollection<LobbyDto> _lobbies;

        public ObservableCollection<LobbyDto> Lobbies
        {
            get { return _lobbies; }
            set { SetProperty(ref _lobbies, value); }
        }


        private DelegateCommand<int?> _joinLobbyCommand;
        private DelegateCommand _createLobbyCommand;
        private DelegateCommand _refreshLobbiesCommand;
        private DelegateCommand _checkChangesCommand;

        public DelegateCommand CheckChangesCommand => _checkChangesCommand ??= new(CheckChanges);

        public DelegateCommand<int?> JoinLobbyCommand => _joinLobbyCommand ??= new DelegateCommand<int?>(JoinLobby, CanJoinLobby);
        public DelegateCommand CreateLobbyCommand => _createLobbyCommand ??= new DelegateCommand(CreateLobby, CanCreateLobby);
        public DelegateCommand RefreshLobbiesCommand => _refreshLobbiesCommand ??= new DelegateCommand(RefreshLobbies);

        public override DelegateCommand LoadCommand => _loadCommand ??= new DelegateCommand(async () => await LoadAsync());


        public LobbiesViewModel(IRegionNavigationService regionNavigationService, IAuthenticator authenticator, IMapper mapper, ILobbyService lobbyService, IUserService userService)
            : base(regionNavigationService, authenticator, mapper)
        {
            _lobbyService = lobbyService ??
                throw new ArgumentNullException(nameof(lobbyService));
            _userService = userService ??
                throw new ArgumentNullException(nameof(userService));
        }

        public void CheckChanges()
        {
            JoinLobbyCommand.RaiseCanExecuteChanged();
        }

        public bool CanJoinLobby(int? id)
        {
            if (id is null) return false;
            if (id == 0) return false;
            return _lobbyService.PlayerCanJoin((int)id, _authenticator.User.Id);
        }

        public void JoinLobby(int? id)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { Global.LOBBY_ID, id }
            };
            Navigate(ViewsEnum.LobbyCreationView, parameters);
            // TODO : Ajouté le joueur dans la base de donnée
        }

        public async void RefreshLobbies()
        {
            Lobbies = new(await _lobbyService.GetLobbiesAsync());
        }

        public bool CanCreateLobby()
        {
            try
            {
                UserDto user = Task.Run(async () => await _userService.GetUserAsync(_authenticator.User.Id)).Result;
                if (user is null) return false;
                return true;
            }
            catch (NullReferenceException e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }

        public void CreateLobby()
        {
            Navigate(ViewsEnum.LobbyCreationView);
        }

        protected override async Task LoadAsync()
        {
            Lobbies = new ObservableCollection<LobbyDto>(await _lobbyService.GetLobbiesAsync());
        }
    }
}
