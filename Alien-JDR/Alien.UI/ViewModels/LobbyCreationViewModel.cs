using Alien.BLL.Dtos;
using Alien.BLL.Interfaces;
using Alien.Socket.Models;
using Alien.UI.Helpers;
using Alien.UI.Models;
using Alien.UI.States;
using AutoMapper;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Alien.UI.ViewModels
{
    public class LobbyCreationViewModel : ViewModelBase, IJournalAware
    {
        private readonly ILobbyService _lobbyService;
        private readonly ICharacterService _characterService;
        private readonly IUserService _userService;
        private readonly INotificationService _notificationService;
        private readonly ILobbyPlayerService _lobbyPlayerService;

        public LobbyModel Lobby { get; set; }
        public SocketRouter SocketRouteur { get; set; } = new SocketRouter();

        private bool _isCreator;

        public bool IsCreator
        {
            get { return _isCreator; }
            set { SetProperty(ref _isCreator, value); }
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
                SetProperty(ref _selectedGameMode, value);
                Lobby.Mode = value.ToString();
                UpdateLobbyAsync();
            }
        }

        private string _lobbyName = $"NOUVELLE PARTIE { new Random().Next(1, 250) }";

        public string LobbyName
        {
            get { return _lobbyName; }
            set
            {
                SetProperty(ref _lobbyName, value);
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
                SetProperty(ref _maximumPlayers, value);
                Lobby.MaximumPlayers = int.Parse(value);
                UpdateLobbyAsync();
            }
        }

        private ObservableCollection<LobbyPlayerModel> _lobbyPlayers = new();

        public ObservableCollection<LobbyPlayerModel> LobbyPlayers
        {
            get { return _lobbyPlayers; }
            set { SetProperty(ref _lobbyPlayers, value); }
        }

        private ObservableCollection<LobbyUserModel> _availableUsers = new();

        public ObservableCollection<LobbyUserModel> AvailableUsers
        {
            get { return _availableUsers; }
            set { SetProperty(ref _availableUsers, value); }
        }

        private LobbyUserModel _selectedUser;

        public LobbyUserModel SelectedUser
        {
            get { return _selectedUser; }
            set { SetProperty(ref _selectedUser, value); }
        }

        private int myVar;

        public int MyProperty
        {
            get { return myVar; }
            set { myVar = value; }
        }

        private ObservableCollection<LobbyCharacterModel> _availableCharacters = new();

        public ObservableCollection<LobbyCharacterModel> AvailableCharacters
        {
            get { return _availableCharacters; }
            set { SetProperty(ref _availableCharacters, value); }
        }

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
            ICharacterService characterService, IUserService userService, INotificationService notificationService, ILobbyPlayerService lobbyPlayerService)
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
            _lobbyPlayerService = lobbyPlayerService ??
                throw new ArgumentNullException(nameof(lobbyPlayerService));

            SocketRouteur = new SocketRouter();
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

        }

        public override void OnNavigatedFrom(NavigationContext navigationContext)
        {
            base.OnNavigatedFrom(navigationContext);

            LobbyPlayerModel player = LobbyPlayers.FirstOrDefault(lb => lb.UserId == _authenticator.User.Id);

            if (player is not null)
            {
                LobbyPlayers.Remove(player);
            }
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            int? lobbyId = navigationContext.Parameters.GetValue<int?>(Global.LOBBY_ID);

            if (!InitializeLobby(lobbyId))
            {
                Navigate(ViewsEnum.LobbiesView);
                _regionNavigationService.Journal.Clear();
            }
        }

        #region Host Socket functions
        public bool PlayerArrived(dynamic cli, Message args)
        {
            try
            {
                // TODO : Déserializer l'objet dans un type anonynme ou un objet "intermédiaire"
                LobbyPlayerModel playerModel = JsonConvert.DeserializeObject<LobbyPlayerModel>(args.message);
                if (playerModel is null) return false;

                LobbyPlayerModel existingPlayer = LobbyPlayers.FirstOrDefault(lb => lb.UserId == playerModel.UserId);

                if (existingPlayer is null)
                {
                    LobbyPlayers.Add(playerModel);
                }
                else
                {
                    existingPlayer = playerModel;
                }
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
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
                SocketRouteur.Start();

                SocketRouteur.On(Global.LOBBY_PLAYER_ARRIVED_CHANNEL, (dynamic cli, Message args) =>
                {
                    Debug.WriteLine("Message reçu :");
                    Debug.WriteLine(args.message);
                    return true;
                });

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

                // TODO: sérialiser l'objet à travers un objet anonyme ou un objet intermédiaire
                string message = JsonConvert.SerializeObject(lobbyPlayer);
                //string message = lobbyPlayer.UserId.ToString();

                SocketRouteur.SendOn(Global.LOBBY_PLAYER_ARRIVED_CHANNEL, message);

                //    .OnReply((dynamic cli, Message arg) =>
                //{
                //    Console.WriteLine($"Callback sur la reception du message Ping : ${arg.message}");
                //});

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
                    
                    
                //    (dynamic cli, Message args) =>
                //{
                //    Debug.WriteLine("Message reçu :");
                //    Debug.WriteLine(args.message);
                //    return true;
                //});
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

                if (_lobbyPlayerService.CreateLobbyPlayer(createdLobbyPlayer)) return true;

                lobbyPlayer = _mapper.Map<LobbyPlayerModel>(Task.Run(async () => await _lobbyPlayerService.GetLobbyPlayerAsync(_authenticator.User.Id, Lobby.Id)).Result);

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

        public bool PersistInHistory()
        {
            return false;
        }
    }
}

