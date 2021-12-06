using Alien.BLL.Dtos;
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
    public class CharacterPublicInforsViewModel : ViewModelBase, IJournalAware
    {
        private readonly ICharacterService _characterService;

        public CharacterCreationDto CharacterCreation { get; set; }

        private CharacterPublicInfosModel _publicCharacter = new();

        public CharacterPublicInfosModel PublicCharacter
        {
            get { return _publicCharacter; }
            set { SetProperty(ref _publicCharacter, value); }
        }

        private RaceEnum _race;

        public RaceEnum Race
        {
            get { return _race; }
            set { SetProperty(ref _race, value); }
        }
        public ObservableCollection<bool> SelectedAttributes { get; set; } = new()
        {
            false,
            false,
            false,
            false
        };


        private DelegateCommand _navigateBackCommand;
        private DelegateCommand _createPublicCharacterCommand;

        public DelegateCommand NavigateBackCommand => _navigateBackCommand ??= new(NavigateBack);
        public DelegateCommand CreatePublicCharacterCommand => _createPublicCharacterCommand ??= new(CreatePublicCharacter);

        public CharacterPublicInforsViewModel(IRegionNavigationService regionNavigationService, IAuthenticator authenticator, ICharacterService characterService)
            : base(regionNavigationService, authenticator)
        {
            _characterService = characterService ??
                throw new ArgumentNullException(nameof(characterService));
        }

        public void NavigateBack()
        {
            if (_regionNavigationService.Journal.CanGoBack)
            {
                _regionNavigationService.Journal.GoBack();
            }
        }

        public async void CreatePublicCharacter()
        {
            // TODO : créer le personnage public
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            CharacterCreation = navigationContext.Parameters.GetValue<CharacterCreationDto>(Global.CHARACTER_CREATION);
        }

        public bool PersistInHistory()
        {
            return true;
        }
    }
}
