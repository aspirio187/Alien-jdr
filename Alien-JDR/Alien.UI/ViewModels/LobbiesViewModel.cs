using Alien.BLL.Dtos;
using Alien.BLL.Interfaces;
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
    public class LobbiesViewModel : ViewModelBase
    {
        private readonly ILobbyService _lobbyService;

        public ObservableCollection<LobbyDto> Lobbies { get; set; }

        public override DelegateCommand LoadCommand => _loadCommand ??= new DelegateCommand(async () => await LoadAsync());

        public LobbiesViewModel(IRegionNavigationService regionNavigationService, IAuthenticator authenticator, ILobbyService lobbyService)
            : base(regionNavigationService, authenticator)
        {
            _lobbyService = lobbyService ??
                throw new ArgumentNullException(nameof(lobbyService));
        }

        protected override async Task LoadAsync()
        {
            Lobbies = new ObservableCollection<LobbyDto>(await _lobbyService.GetLobbiesAsync());
        }
    }
}
