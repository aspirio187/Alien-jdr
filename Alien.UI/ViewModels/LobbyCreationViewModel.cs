using Alien.BLL.Dtos;
using Alien.BLL.Interfaces;
using Alien.Socket.Models;
using Alien.UI.Commands;
using Alien.UI.Helpers;
using Alien.UI.Managers;
using Alien.UI.Models;
using Alien.UI.States;
using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Alien.UI.ViewModels
{
    public class LobbyCreationViewModel : ViewModelBase
    {
        private readonly ILobbyService _lobbyService;
        private readonly ICharacterService _characterService;
        private readonly IUserService _userService;
        private readonly INotificationService _notificationService;
        private readonly ILobbyPlayerService _lobbyPlayerService;
        private readonly NavigationManager _navigationManager;

        public LobbyModel Lobby { get; set; }
        public SocketRouter SocketRouteur { get; set; } = new SocketRouter();

        private bool _isCreator;

        public bool IsCreator
        {
            get { return _isCreator; }
            set
            {
                _isCreator = value;
                NotifyPropertyChanged();
            }
        }

        public Guid UserId
        {
            get { return _authenticator.User.Id; }
        }

        private LobbyModeEnum _selectedGameMode;

        public LobbyModeEnum SelectedGameMode
        {
            get { return _selectedGameMode; }
            set
            {
                _selectedGameMode = value;
                NotifyPropertyChanged();
                Lobby.Mode = value.ToString();
                UpdateLobbyAsync();
            }
        }

        private string _lobbyName = $"NOUVELLE PARTIE {new Random().Next(1, 250)}";

        public string LobbyName
        {
            get { return _lobbyName; }
            set
            {
                _lobbyName = value;
                NotifyPropertyChanged();
                Lobby.Name = value;
                UpdateLobbyAsync();
            }
        }

        private string _maximumPlayers = "6";

        public string MaximumPlayers
        {
            get { return _maximumPlayers; }
            set
            {
                _maximumPlayers = value;
                NotifyPropertyChanged();
                Lobby.MaximumPlayers = int.Parse(value);
                UpdateLobbyAsync();
            }
        }

        public object Lock { get; private set; } = new object();

        private ObservableCollection<LobbyPlayerModel> _lobbyPlayers = new();

        public ObservableCollection<LobbyPlayerModel> LobbyPlayers
        {
            get { return _lobbyPlayers; }
            set
            {
                _lobbyPlayers = value;
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<LobbyUserModel> _availableUsers = new();

        public ObservableCollection<LobbyUserModel> AvailableUsers
        {
            get { return _availableUsers; }
            set
            {
                _availableUsers = value;
                NotifyPropertyChanged();
            }
        }

        private LobbyUserModel _selectedUser;

        public LobbyUserModel SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                _selectedUser = value;
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<LobbyCharacterModel> _availableCharacters = new();

        public ObservableCollection<LobbyCharacterModel> AvailableCharacters
        {
            get { return _availableCharacters; }
            set
            {
                _availableCharacters = value;
                NotifyPropertyChanged();
            }
        }

        private LobbyCharacterModel _selectedNpcCharacter;

        public LobbyCharacterModel SelectedNpcCharacter
        {
            get { return _selectedNpcCharacter; }
            set
            {
                _selectedNpcCharacter = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand LoadPlayersCommand { get; private set; }
        public ICommand InvitePlayerCommand { get; private set; }
        public ICommand LoadCharactersCommand { get; private set; }
        public ICommand AddNpcCharacterCommand { get; private set; }
        public ICommand DeclareArrivalCommand { get; private set; }
        public ICommand StartGameCommand { get; private set; }


        public LobbyCreationViewModel(IAuthenticator authenticator, IMapper mapper, ILobbyService lobbyService,
            ICharacterService characterService, IUserService userService, INotificationService notificationService,
            ILobbyPlayerService lobbyPlayerService, NavigationManager navigationManager)
            : base(authenticator, mapper)
        {
            if (lobbyService is null)
            {
                throw new ArgumentNullException(nameof(lobbyService));
            }

            if (characterService is null)
            {
                throw new ArgumentNullException(nameof(characterService));
            }

            if (userService is null)
            {
                throw new ArgumentNullException(nameof(userService));
            }

            if (notificationService is null)
            {
                throw new ArgumentNullException(nameof(notificationService));
            }

            if (lobbyPlayerService is null)
            {
                throw new ArgumentNullException(nameof(lobbyPlayerService));
            }

            if (navigationManager is null)
            {
                throw new ArgumentNullException(nameof(navigationManager));
            }

            _lobbyService = lobbyService;
            _characterService = characterService;
            _userService = userService;
            _notificationService = notificationService;
            _lobbyPlayerService = lobbyPlayerService;
            _navigationManager = navigationManager;

            LoadPlayersCommand = new RelayCommand(LoadPlayers);
            InvitePlayerCommand = new RelayCommand(InvitePlayer);
            LoadCharactersCommand = new RelayCommand(LoadCharacters);
            AddNpcCharacterCommand = new RelayCommand(AddNpcCharacter);
            DeclareArrivalCommand = new RelayCommand(async () => await DeclareArrival());
            StartGameCommand = new RelayCommand(StartGame);

            BindingOperations.EnableCollectionSynchronization(_lobbyPlayers, Lock);
        }

        public async Task UpdateLobbyAsync()
        {
            LobbyDto lobbyFromRepo = await _lobbyService.GetLobby(Lobby.Id);
            lobbyFromRepo = _mapper.Map<LobbyDto>(Lobby);
            _lobbyService.UpdateLobby(lobbyFromRepo);
        }

        public async void LoadPlayers()
        {
            List<LobbyUserModel> users = _mapper.Map<List<LobbyUserModel>>(await _userService.GetUsersAsync());

            users.Remove(users.FirstOrDefault(u => u.Id == _authenticator.User.Id));

            foreach (LobbyPlayerModel player in LobbyPlayers)
            {
                users.Remove(users.FirstOrDefault(u => u.Id == player.UserId));
            }

            AvailableUsers = new(users);
        }

        public void InvitePlayer()
        {
            if (SelectedUser is not null && SelectedUser.IsValid)
            {
                CreateNotificationDto notification = new CreateNotificationDto()
                {
                    LobbyId = Lobby.Id,
                    ReceiverId = SelectedUser.Id,
                    SenderId = _authenticator.User.Id
                };
                _notificationService.SendNotification(notification);
                AvailableUsers.Clear();
            }
            // TODO : Send notification to user
        }

        public async void LoadCharacters()
        {
            IEnumerable<CharacterMiniatureDto> charactersFromRepo = await _characterService.GetAvailableCharactersAsync(_authenticator.User.Id);
            AvailableCharacters = new(_mapper.Map<IEnumerable<LobbyCharacterModel>>(charactersFromRepo));
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
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { Global.LOBBY_ID, Lobby.Id },
            };

            if (IsCreator)
            {
                // Navigue vers GmInGameView
            }
            else
            {
                // Navigue vers InGameView
            }
        }

        //public override void OnNavigatedFrom(NavigationContext navigationContext)
        //{
        //    base.OnNavigatedFrom(navigationContext);

        //    LobbyPlayerModel player = LobbyPlayers.FirstOrDefault(lb => lb.UserId == _authenticator.User.Id);

        //    if (player is not null)
        //    {
        //        LobbyPlayers.Remove(player);
        //    }
        //}

        //public override void OnNavigatedTo(NavigationContext navigationContext)
        //{
        //    base.OnNavigatedTo(navigationContext);

        //    int? lobbyId = navigationContext.Parameters.GetValue<int?>(Global.LOBBY_ID);

        //    if (!InitializeLobby(lobbyId))
        //    {
        //        Navigate(ViewsEnum.LobbiesView);
        //        _regionNavigationService.Journal.Clear();
        //    }
        //}

        #region Host Socket functions
        public bool PlayerArrived(dynamic cli, Message args)
        {
            try
            {
                // TODO : Déserializer l'objet dans un type anonynme ou un objet "intermédiaire"
                LobbyPlayerArrival arrivedPlayer = JsonConvert.DeserializeObject<LobbyPlayerArrival>(args.message);
                LobbyPlayerModel playerModel = _mapper.Map<LobbyPlayerModel>(arrivedPlayer);
                if (playerModel is null) return false;

                LobbyPlayerModel existingPlayer = LobbyPlayers.FirstOrDefault(lb => lb.UserId == playerModel.UserId);

                lock (Lock)
                {
                    if (existingPlayer is null)
                    {
                        LobbyPlayers.Add(playerModel);
                    }
                    else
                    {
                        existingPlayer = playerModel;
                    }
                }

                CancellationTokenSource cts = new CancellationTokenSource(1000);
                Task.Run(() =>
                {
                    SocketRouteur.EmitOn(Global.LOBBY_PLAYER_ARRIVED_CHANNEL, args.message);
                }, cts.Token);
                if (cts.IsCancellationRequested) Debug.WriteLine("Fonction annulée");



                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }

        public bool DeclareOtherPlayer(dynamic cli, Message args)
        {
            try
            {
                LobbyPlayerArrival arrivedPlayer = JsonConvert.DeserializeObject<LobbyPlayerArrival>(args.message);
                if (arrivedPlayer.UserId == _authenticator.User.Id) return false;
                LobbyPlayerModel playerModel = _mapper.Map<LobbyPlayerModel>(arrivedPlayer);
                if (playerModel is null) return false;

                LobbyPlayerModel existingPlayer = LobbyPlayers.FirstOrDefault(lb => lb.UserId == playerModel.UserId);

                lock (Lock)
                {
                    if (existingPlayer is null)
                    {
                        LobbyPlayers.Add(playerModel);
                    }
                    else
                    {
                        existingPlayer = playerModel;
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }

        public async Task DeclareArrival()
        {
            CancellationTokenSource cts = new CancellationTokenSource(10000);
            await Task.Factory.StartNew(() =>
            {

                LobbyPlayerModel lobbyPlayer = _mapper.Map<LobbyPlayerModel>(Task.Run(async () => await _lobbyPlayerService.GetLobbyPlayerAsync(_authenticator.User.Id, Lobby.Id)).Result);

                if (!Task.Run(async () => await _lobbyService.PlayerIsHost(Lobby.Id, _authenticator.User.Id)).Result)
                {
                    LobbyPlayerArrival playerArrived = _mapper.Map<LobbyPlayerArrival>(lobbyPlayer);

                    string message = JsonConvert.SerializeObject(playerArrived);
                    SocketRouteur.SendOn(Global.LOBBY_PLAYER_ARRIVED_CHANNEL, message).OnReply((dynamic cli, Message args) =>
                    {
                        Debug.WriteLine(args.message);
                    });

                }
            }, cts.Token);
        }
        #endregion

        #region Initialization of the lobby
        // Initialise le lobby s'il est null. S'il ne l'est pas alors il tente de charger un lobby existant
        private bool InitializeLobby(int? lobbyId)
        {
            if (lobbyId is not null) return LoadExistingLobby(lobbyId);

            try
            {
                string hostIp = Dns.GetHostEntry(Dns.GetHostName()).AddressList.FirstOrDefault(ip => ip.IsIPv6LinkLocal)?.ToString();

                hostIp = hostIp.Remove(hostIp.IndexOf('%'));

                Lobby = _mapper.Map<LobbyModel>(_lobbyService.CreateLobby(new CreateLobbyDto()
                {
                    HostIp = hostIp,
                    MaximumPlayers = int.Parse(MaximumPlayers),
                    Mode = SelectedGameMode.ToString(),
                    Name = LobbyName
                }));

                if (Lobby is null) return false;

                CreateLobbyPlayerDto creator = new CreateLobbyPlayerDto()
                {
                    UserId = _authenticator.User.Id,
                    LobbyId = Lobby.Id,
                    CharacterId = null,
                    IsCreator = true
                };

                if (!_lobbyPlayerService.CreateLobbyCreator(creator))
                {
                    _lobbyService.DeleteLobby(Lobby.Id);
                    return false;
                }

                IsCreator = true;
                SocketRouteur = SocketRouteur.Start();

                SocketRouteur.On(Global.LOBBY_PLAYER_ARRIVED_CHANNEL, PlayerArrived);

                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }

        // Charge un lobby existant
        private bool LoadExistingLobby(int? lobbyId)
        {
            try
            {
                Lobby = _mapper.Map<LobbyModel>(Task.Run(async () => await _lobbyService.GetLobby((int)lobbyId)).Result);

                if (Lobby is null || Lobby.Id <= 0) return false;

                SelectedGameMode = (LobbyModeEnum)Enum.Parse(typeof(LobbyModeEnum), Lobby.Mode);
                LobbyName = Lobby.Name;
                MaximumPlayers = Lobby.MaximumPlayers.ToString();

                if (LoadExistingCreatorLobby(lobbyId)) return true;

                IsCreator = false;

                LobbyPlayerModel lobbyPlayer = _mapper.Map<LobbyPlayerModel>(Task.Run(async () => await _lobbyPlayerService.GetLobbyPlayerAsync(_authenticator.User.Id, (int)lobbyId)).Result);

                if (lobbyPlayer is null)
                {
                    if (!LoadLobbyPlayer(ref lobbyPlayer)) return false;
                }

                LobbyPlayers.Add(lobbyPlayer);

                try
                {
                    SocketRouteur = SocketRouteur.Start().Subscribe(Lobby.HostIp);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }

                SocketRouteur.On(Global.LOBBY_PLAYER_ARRIVED_CHANNEL, DeclareOtherPlayer);

                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }

        private bool LoadExistingCreatorLobby(int? lobbyId)
        {
            try
            {
                if (!Task.Run(async () => await _lobbyService.PlayerIsHost((int)lobbyId, _authenticator.User.Id)).Result) return false;

                string hostIp = Dns.GetHostEntry(Dns.GetHostName()).AddressList.FirstOrDefault(ip => ip.IsIPv6LinkLocal)?.ToString();

                hostIp = hostIp.Remove(hostIp.IndexOf('%'));
                Lobby.HostIp = hostIp;

                if (!_lobbyService.UpdateHostIp(Lobby.Id, Lobby.HostIp)) return false;

                IsCreator = true;
                SocketRouteur.Start();
                SocketRouteur.On(Global.LOBBY_PLAYER_ARRIVED_CHANNEL, PlayerArrived);
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }

        private bool LoadLobbyPlayer(ref LobbyPlayerModel lobbyPlayer)
        {
            try
            {
                CreateLobbyPlayerDto createdLobbyPlayer = new CreateLobbyPlayerDto()
                {
                    UserId = _authenticator.User.Id,
                    CharacterId = null,
                    IsCreator = false,
                    LobbyId = Lobby.Id
                };

                lobbyPlayer = _mapper.Map<LobbyPlayerModel>(_lobbyPlayerService.CreateLobbyPlayer(createdLobbyPlayer));
                if (lobbyPlayer is null) return false;
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }
        #endregion
    }
}

