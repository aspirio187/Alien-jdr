using Alien.BLL.Dtos;
using Alien.BLL.Interfaces;
using Alien.UI.Helpers;
using Alien.UI.States;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.UI.ViewModels
{
    public class CharacterCreationSummaryViewModel : ViewModelBase, IJournalAware
    {
        private readonly ICharacterService _characterService;

        private CharacterCreationDto _characterCreation;

        public CharacterCreationDto CharacterCreation
        {
            get { return _characterCreation; }
            set { SetProperty(ref _characterCreation, value); }
        }

        private string _careerDescription;

        public string CareerDescription
        {
            get { return _careerDescription; }
            set { SetProperty(ref _careerDescription, value); }
        }

        public CharacterCreationSummaryViewModel(IRegionNavigationService regionNavigationService, IAuthenticator authenticator, ICharacterService characterService)
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

        public void CreateCharacter()
        {

        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            CharacterCreation = navigationContext.Parameters.GetValue<CharacterCreationDto>(Global.CHARACTER_CREATION);
            if (CharacterCreation is not null && !string.IsNullOrEmpty(CharacterCreation.Career))
            {
                CareerDescription = _characterService.GetCareer(CharacterCreation.Career).Description;
            }
        }

        public bool PersistInHistory()
        {
            return true;
        }
    }
}
