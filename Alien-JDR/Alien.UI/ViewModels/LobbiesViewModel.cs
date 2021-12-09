using Alien.BLL.Dtos;
using Alien.BLL.Interfaces;
using Alien.UI.Helpers;
using Alien.UI.States;
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

        public ObservableCollection<LobbyDto> Lobbies { get; set; }

        private DelegateCommand<int?> _joinLobbyCommand;
        private DelegateCommand _createLobbyCommand;

        public DelegateCommand<int?> JoinLobbyCommand => _joinLobbyCommand ??= new DelegateCommand<int?>(JoinLobby, CanJoinLobby);
        public override DelegateCommand LoadCommand => _loadCommand ??= new DelegateCommand(async () => await LoadAsync());
        public DelegateCommand CreateLobbyCommand
        {
            get { return _createLobbyCommand; }
            set { _createLobbyCommand = value; }
        }


        public LobbiesViewModel(IRegionNavigationService regionNavigationService, IAuthenticator authenticator, ILobbyService lobbyService, IUserService userService)
            : base(regionNavigationService, authenticator)
        {
            _lobbyService = lobbyService ??
                throw new ArgumentNullException(nameof(lobbyService));
            _userService = userService ??
                throw new ArgumentNullException(nameof(userService));
        }

        public bool CanJoinLobby(int? id)
        {
            if (id is null) return false;
            if (id == 0) return false;
            return true;
        }

        public void JoinLobby(int? id)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { Global.LOBBY_ID, id }
            };
            // TODO : Ajouté le joueur dans la base de donnée
        }

        public async Task<bool> CanCreateLobby()
        {
            try
            {
                UserDto user = await _userService.GetUserAsync(_authenticator.User.Id);
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
