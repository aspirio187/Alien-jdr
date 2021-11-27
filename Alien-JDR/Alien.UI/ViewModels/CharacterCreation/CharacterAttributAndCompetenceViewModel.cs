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
    public enum Attributes
    {
        Strenght,
        Agility,
        Mind,
        Empathy
    }

    public class CharacterAttributAndCompetenceViewModel : ViewModelBase, IJournalAware
    {
        private readonly ICharacterService _characterService;

        public CharacterCreationDto CharacterCreation { get; set; }

        private int _attributePoints;

        public int AttributePoints
        {
            get { return _attributePoints; }
            set { SetProperty(ref _attributePoints, value); }
        }

        private int _competencePoints;

        public int CompetencePoints
        {
            get { return _competencePoints; }
            set { SetProperty(ref _competencePoints, value); }
        }

        public CharacterAttributAndCompetenceViewModel(IRegionNavigationService regionNavigationService, IAuthenticator authenticator, ICharacterService characterService)
            : base(regionNavigationService, authenticator)
        {
            _characterService = characterService ??
                throw new ArgumentNullException(nameof(characterService));

            AttributePoints = 14;
            CompetencePoints = 10;
        }

        public void AddAtribute(Attributes attribute)
        {
            switch (attribute)
            {
                case Attributes.Strenght:
                    break;
                case Attributes.Agility:
                    break;
                case Attributes.Mind:
                    break;
                case Attributes.Empathy:
                    break;
                default: break;
            }
        }

        public void NavigateBack()
        {
            if (_regionNavigationService.Journal.CanGoBack)
            {
                _regionNavigationService.Journal.GoBack();
            }
        }

        public void NavigateNextPage()
        {
            if (CharacterCreation.Race.Equals(RaceEnum.Android.ToString()))
            {
                // TODO : Navigue vers la page pour les android
            }
            else
            {
                // TODO : Navigue vers la page pour récapitulative
            }
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
