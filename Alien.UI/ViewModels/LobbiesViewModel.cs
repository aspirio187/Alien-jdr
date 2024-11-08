using Alien.BLL.Dtos;
using Alien.BLL.Interfaces;
using Alien.UI.Commands;
using Alien.UI.Helpers;
using Alien.UI.Managers;
using Alien.UI.States;
using Alien.UI.Views;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Alien.UI.ViewModels
{
    public class LobbiesViewModel : ViewModelBase
    {
        private readonly ILobbyService _lobbyService;
        private readonly IUserService _userService;
        private readonly NavigationManager _navigationManager;

        //public ObservableCollection<LobbyDto> Lobbies { get; set; }
        private ObservableCollection<LobbyDto> _lobbies = new();

        public ObservableCollection<LobbyDto> Lobbies
        {
            get { return _lobbies; }
            set
            {
                _lobbies = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand CheckChangesCommand { get; private set; }

        public ICommand JoinLobbyCommand { get; private set; }
        public ICommand CreateLobbyCommand { get; private set; }
        public ICommand RefreshLobbiesCommand { get; private set; }

        public ICommand LoadCommand { get; private set; }


        public LobbiesViewModel(IAuthenticator authenticator, IMapper mapper, ILobbyService lobbyService, IUserService userService, NavigationManager navigationManager)
            : base(authenticator, mapper)
        {
            if (lobbyService is null)
            {
                throw new ArgumentNullException(nameof(lobbyService));
            }

            if (userService is null)
            {
                throw new ArgumentNullException(nameof(userService));
            }

            if (navigationManager is null)
            {
                throw new ArgumentNullException(nameof(navigationManager));
            }

            _lobbyService = lobbyService;
            _userService = userService;
            _navigationManager = navigationManager;

            CheckChangesCommand = new RelayCommand(CheckChanges);
            JoinLobbyCommand = new RelayCommand<int?>(JoinLobby, CanJoinLobby);
            CreateLobbyCommand = new RelayCommand(CreateLobby, CanCreateLobby);
            RefreshLobbiesCommand = new RelayCommand(RefreshLobbies);
            LoadCommand = new RelayCommand(async () => await LoadAsync());
        }

        public void CheckChanges()
        {
            // TODO : Vérifier les changements dans les lobbies
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
            _navigationManager.Navigate(nameof(LobbyCreationView), parameters: parameters);
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
            _navigationManager.Navigate(nameof(LobbyCreationView));
        }

        public async Task LoadAsync()
        {
            Lobbies = new ObservableCollection<LobbyDto>(await _lobbyService.GetLobbiesAsync());
        }
    }
}
