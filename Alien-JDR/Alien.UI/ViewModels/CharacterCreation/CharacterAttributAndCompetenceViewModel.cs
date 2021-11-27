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
    public enum Attributes
    {
        Force,
        Agilité,
        Esprit,
        Empathie
    }

    public enum Competences
    {

    }

    public class CharacterAttributAndCompetenceViewModel : ViewModelBase, IJournalAware
    {
        private readonly ICharacterService _characterService;

        public CharacterCreationDto CharacterCreation { get; set; }

        private CharacterAttributesCompetencesModel _characterAttributesCompetences;

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

        private DelegateCommand<Attributes> _increaseAttributeCommand;
        private DelegateCommand<Attributes> _decreaseAttributeCommand;

        public DelegateCommand<Attributes> IncreaseAttributeCommand => _increaseAttributeCommand ??= new DelegateCommand<Attributes>(IncreaseAttribute, CanIncreaseAttributes);
        public DelegateCommand<Attributes> DecreaseAttributeCommand => _decreaseAttributeCommand ??= new DelegateCommand<Attributes>(DecreaseAttribute);

        public CharacterAttributAndCompetenceViewModel(IRegionNavigationService regionNavigationService, IAuthenticator authenticator, ICharacterService characterService)
            : base(regionNavigationService, authenticator)
        {
            _characterService = characterService ??
                throw new ArgumentNullException(nameof(characterService));

            AttributePoints = 14;
            CompetencePoints = 10;
        }

        public bool CanIncreaseAttributes(Attributes attribute)
        {
            if (AttributePoints <= 0) return false;

            switch (attribute)
            {
                case Attributes.Force:
                    if(CharacterAttributesCompetences.Strength )
                    break;
                case Attributes.Agilité:
                    break;
                case Attributes.Esprit:
                    break;
                case Attributes.Empathie:
                    break;
                default:
                    break;
            }
        }

        public void IncreaseAttribute(Attributes attribute)
        {
            switch (attribute)
            {
                case Attributes.Force:
                    if (IsKeyAttribute(attribute))
                    {
                        if ()
                    }
                    break;
                case Attributes.Agilité:
                    break;
                case Attributes.Esprit:
                    break;
                case Attributes.Empathie:
                    break;
                default:
                    break;
            }
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
        }

        public bool PersistInHistory()
        {
            return true;
        }

        private bool IsKeyAttribute(Attributes attribute)
        {
            string characterKeyAttribute = _characterService.GetCareer(CharacterCreation.Career).KeyAttribute;
            if (Equals(characterKeyAttribute, attribute.ToString())) return true;
            return false;
        }
    }
}
