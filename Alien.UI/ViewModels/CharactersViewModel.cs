using Alien.BLL.Dtos;
using Alien.BLL.Interfaces;
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
    public class CharactersViewModel : ViewModelBase
    {
        private readonly ICharacterService _characterService;

        private DelegateCommand _navigateCreateCharacterCommand;

        //public ObservableCollection<CharacterMiniatureDto> CharacterMiniatures { get; set; }
        private ObservableCollection<CharacterMiniatureDto> _characterMiniatures;

        public ObservableCollection<CharacterMiniatureDto> CharacterMiniatures
        {
            get { return _characterMiniatures; }
            set {SetProperty(ref _characterMiniatures, value); }
        }


        public override DelegateCommand LoadCommand => _loadCommand ??= new(async () => await LoadAsync());
        public DelegateCommand NavigateCreateCharacterCommand => _navigateCreateCharacterCommand ??= new DelegateCommand(NavigateCreateCharacter);

        public CharactersViewModel(IRegionNavigationService regionNavigationService, IAuthenticator authenticator, IMapper mapper, ICharacterService characterService)
            : base(regionNavigationService, authenticator, mapper)
        {
            _characterService = characterService ??
                throw new ArgumentNullException(nameof(characterService));
        }

        protected override async Task LoadAsync()
        {
            try
            {
                CharacterMiniatures = new(await _characterService.GetCharactersMiniaturesAsync(_authenticator.User.Id));
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        public void NavigateCreateCharacter()
        {
            Navigate(Helpers.ViewsEnum.CharacterCareerSelectionView);
        }
    }
}
