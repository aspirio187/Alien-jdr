using Alien.BLL.Dtos;
using Alien.BLL.Interfaces;
using Alien.Tools.Helpers;
using Alien.UI.Helpers;
using Alien.UI.Models;
using Alien.UI.States;
using Prism.Commands;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.UI.ViewModels
{
    public enum Competences
    {

    }

    public class CharacterAttributAndCompetenceViewModel : ViewModelBase, IJournalAware
    {
        private readonly ICharacterService _characterService;

        public CharacterCreationDto CharacterCreation { get; set; }

        private CharacterAttributesCompetencesModel _characterAttributesCompetences = new();

        public CharacterAttributesCompetencesModel CharacterAttributesCompetences
        {
            get { return _characterAttributesCompetences; }
            set { SetProperty(ref _characterAttributesCompetences, value); }
        }

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

        private DelegateCommand<Attributes?> _increaseAttributeCommand;
        private DelegateCommand<Attributes> _decreaseAttributeCommand;

        public DelegateCommand<Attributes?> IncreaseAttributeCommand => _increaseAttributeCommand ??= new DelegateCommand<Attributes?>(IncreaseAttribute, CanIncreaseAttributes);
        public DelegateCommand<Attributes> DecreaseAttributeCommand => _decreaseAttributeCommand ??= new DelegateCommand<Attributes>(DecreaseAttribute);

        public CharacterAttributAndCompetenceViewModel(IRegionNavigationService regionNavigationService, IAuthenticator authenticator, ICharacterService characterService)
            : base(regionNavigationService, authenticator)
        {
            _characterService = characterService ??
                throw new ArgumentNullException(nameof(characterService));

            AttributePoints = 14;
            CompetencePoints = 10;
        }

        public bool CanIncreaseAttributes(Attributes? attribute)
        {
            return AttributePoints <= 0
                ? false
                : attribute is null
                ? false
                : attribute switch
                {
                    Attributes.Force => IsKeyAttribute(attribute) ? CharacterAttributesCompetences.Strength < 5 : CharacterAttributesCompetences.Strength < 4,
                    Attributes.Agilité => IsKeyAttribute(attribute) ? CharacterAttributesCompetences.Agility < 5 : CharacterAttributesCompetences.Agility < 4,
                    Attributes.Esprit => IsKeyAttribute(attribute) ? CharacterAttributesCompetences.Mind < 5 : CharacterAttributesCompetences.Mind < 4,
                    Attributes.Empathie => IsKeyAttribute(attribute) ? CharacterAttributesCompetences.Empathy < 5 : CharacterAttributesCompetences.Empathy < 4,
                    _ => false,
                };
        }

        public void IncreaseAttribute(Attributes? attribute)
        {
            switch (attribute)
            {
                case Attributes.Force:
                    CharacterAttributesCompetences.Strength += 1;
                    break;
                case Attributes.Agilité:
                    CharacterAttributesCompetences.Agility += 1;
                    break;
                case Attributes.Esprit:
                    CharacterAttributesCompetences.Mind += 1;
                    break;
                case Attributes.Empathie:
                    CharacterAttributesCompetences.Empathy += 1;
                    break;
                default: break;
            }

            IncreaseAttributeCommand.RaiseCanExecuteChanged();
        }

        public void DecreaseAttribute(Attributes attributes)
        {

        }

        public bool CanIncreaseCompetence()
        {
            return CompetencePoints > 0;
        }

        public void IncreaseCompetence(Competences competences)
        {

        }

        public void DecreaseCompetence(Competences competences)
        {

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
            IncreaseAttributeCommand.RaiseCanExecuteChanged();
        }

        public bool PersistInHistory()
        {
            return true;
        }

        private bool IsKeyAttribute(Attributes? attribute)
        {
            if (CharacterCreation is null) return false;
            if (attribute is null) return false;
            string characterKeyAttribute = _characterService.GetCareer(CharacterCreation.Career).KeyAttribute;
            if (Equals(characterKeyAttribute, attribute.ToString())) return true;
            return false;
        }
    }
}
