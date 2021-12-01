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
        private DelegateCommand<Attributes?> _decreaseAttributeCommand;
        private DelegateCommand<Competences?> _increaseCompetenceCommand;
        private DelegateCommand<Competences?> _decreaseCompetenceCommand;

        public DelegateCommand<Attributes?> IncreaseAttributeCommand => _increaseAttributeCommand ??= new DelegateCommand<Attributes?>(IncreaseAttribute, CanIncreaseAttributes);
        public DelegateCommand<Attributes?> DecreaseAttributeCommand => _decreaseAttributeCommand ??= new DelegateCommand<Attributes?>(DecreaseAttribute, CanDecreaseAttribute);
        public DelegateCommand<Competences?> IncreaseCompetenceCommand => _increaseCompetenceCommand ??= new DelegateCommand<Competences?>(IncreaseCompetence, CanIncreaseCompetence);
        public DelegateCommand<Competences?> DecreaseCompetenceCommand => _decreaseCompetenceCommand ??= new DelegateCommand<Competences?>(DecreaseCompetence, CanDecreaseCompetence);

        public CharacterAttributAndCompetenceViewModel(IRegionNavigationService regionNavigationService, IAuthenticator authenticator, ICharacterService characterService)
            : base(regionNavigationService, authenticator)
        {
            _characterService = characterService ??
                throw new ArgumentNullException(nameof(characterService));

            AttributePoints = 14;
            CompetencePoints = 10;

            IncreaseAttributeCommand.RaiseCanExecuteChanged();
            DecreaseAttributeCommand.RaiseCanExecuteChanged();
            IncreaseCompetenceCommand.RaiseCanExecuteChanged();
            DecreaseCompetenceCommand.RaiseCanExecuteChanged();
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

            AttributePoints--;
            IncreaseAttributeCommand.RaiseCanExecuteChanged();
            DecreaseAttributeCommand.RaiseCanExecuteChanged();
        }

        public bool CanDecreaseAttribute(Attributes? attribute)
        {
            return AttributePoints >= 14
                ? false
                : attribute is null
                ? false
                : attribute switch
                {
                    Attributes.Force => CharacterAttributesCompetences.Strength > 2,
                    Attributes.Agilité => CharacterAttributesCompetences.Agility > 2,
                    Attributes.Esprit => CharacterAttributesCompetences.Mind > 2,
                    Attributes.Empathie => CharacterAttributesCompetences.Empathy > 2,
                    _ => false,
                };
        }

        public void DecreaseAttribute(Attributes? attribute)
        {
            switch (attribute)
            {
                case Attributes.Force:
                    CharacterAttributesCompetences.Strength -= 1;
                    break;
                case Attributes.Agilité:
                    CharacterAttributesCompetences.Agility -= 1;
                    break;
                case Attributes.Esprit:
                    CharacterAttributesCompetences.Mind -= 1;
                    break;
                case Attributes.Empathie:
                    CharacterAttributesCompetences.Empathy -= 1;
                    break;
            }

            AttributePoints++;
            IncreaseAttributeCommand.RaiseCanExecuteChanged();
            DecreaseAttributeCommand.RaiseCanExecuteChanged();
        }

        public bool CanIncreaseCompetence(Competences? competence)
        {
            return CompetencePoints <= 0
                ? false
                : competence is null
                ? false
                : competence switch
                {
                    Competences.HeavyMachines => IsKeyCompetence(competence) ? CharacterAttributesCompetences.HeavyMachines < 3 : CharacterAttributesCompetences.HeavyMachines < 1,
                    Competences.Stamina => IsKeyCompetence(competence) ? CharacterAttributesCompetences.Stamina < 3 : CharacterAttributesCompetences.Stamina < 1,
                    Competences.CloseCombat => IsKeyCompetence(competence) ? CharacterAttributesCompetences.CloseCombat < 3 : CharacterAttributesCompetences.CloseCombat < 1,
                    Competences.Mobility => IsKeyCompetence(competence) ? CharacterAttributesCompetences.Mobility < 3 : CharacterAttributesCompetences.Mobility < 1,
                    Competences.Piloting => IsKeyCompetence(competence) ? CharacterAttributesCompetences.Piloting < 3 : CharacterAttributesCompetences.Piloting < 1,
                    Competences.RangeCombat => IsKeyCompetence(competence) ? CharacterAttributesCompetences.RangeCombat < 3 : CharacterAttributesCompetences.RangeCombat < 1,
                    Competences.Observation => IsKeyCompetence(competence) ? CharacterAttributesCompetences.Observation < 3 : CharacterAttributesCompetences.Observation < 1,
                    Competences.Comtech => IsKeyCompetence(competence) ? CharacterAttributesCompetences.Comtech < 3 : CharacterAttributesCompetences.Comtech < 1,
                    Competences.Survival => IsKeyCompetence(competence) ? CharacterAttributesCompetences.Survival < 3 : CharacterAttributesCompetences.Survival < 1,
                    Competences.Manipulation => IsKeyCompetence(competence) ? CharacterAttributesCompetences.Manipulation < 3 : CharacterAttributesCompetences.Manipulation < 1,
                    Competences.Commandment => IsKeyCompetence(competence) ? CharacterAttributesCompetences.Commandment < 3 : CharacterAttributesCompetences.Commandment < 1,
                    Competences.MedicalCare => IsKeyCompetence(competence) ? CharacterAttributesCompetences.MedicalCare < 3 : CharacterAttributesCompetences.MedicalCare < 1,
                    _ => false,
                };
        }

        public void IncreaseCompetence(Competences? competence)
        {
            switch (competence)
            {
                case Competences.HeavyMachines:
                    CharacterAttributesCompetences.HeavyMachines++;
                    break;
                case Competences.Stamina:
                    CharacterAttributesCompetences.Stamina++;
                    break;
                case Competences.CloseCombat:
                    CharacterAttributesCompetences.CloseCombat++;
                    break;
                case Competences.Mobility:
                    CharacterAttributesCompetences.Mobility++;
                    break;
                case Competences.Piloting:
                    CharacterAttributesCompetences.Piloting++;
                    break;
                case Competences.RangeCombat:
                    CharacterAttributesCompetences.RangeCombat++;
                    break;
                case Competences.Observation:
                    CharacterAttributesCompetences.Observation++;
                    break;
                case Competences.Comtech:
                    CharacterAttributesCompetences.Comtech++;
                    break;
                case Competences.Survival:
                    CharacterAttributesCompetences.Survival++;
                    break;
                case Competences.Manipulation:
                    CharacterAttributesCompetences.Manipulation++;
                    break;
                case Competences.Commandment:
                    CharacterAttributesCompetences.Commandment++;
                    break;
                case Competences.MedicalCare:
                    CharacterAttributesCompetences.MedicalCare++;
                    break;
            }

            CompetencePoints--;
            IncreaseCompetenceCommand.RaiseCanExecuteChanged();
            DecreaseCompetenceCommand.RaiseCanExecuteChanged();
        }

        public bool CanDecreaseCompetence(Competences? competence)
        {
            return CompetencePoints >= 10
                ? false
                : competence is null
                ? false
                : competence switch
                {
                    Competences.HeavyMachines => CharacterAttributesCompetences.HeavyMachines > 0,
                    Competences.Stamina => CharacterAttributesCompetences.Stamina > 0,
                    Competences.CloseCombat => CharacterAttributesCompetences.CloseCombat > 0,
                    Competences.Mobility => CharacterAttributesCompetences.Mobility > 0,
                    Competences.Piloting => CharacterAttributesCompetences.Piloting > 0,
                    Competences.RangeCombat => CharacterAttributesCompetences.RangeCombat > 0,
                    Competences.Observation => CharacterAttributesCompetences.Observation > 0,
                    Competences.Comtech => CharacterAttributesCompetences.Comtech > 0,
                    Competences.Survival => CharacterAttributesCompetences.Survival > 0,
                    Competences.Manipulation => CharacterAttributesCompetences.Manipulation > 0,
                    Competences.Commandment => CharacterAttributesCompetences.Commandment > 0,
                    Competences.MedicalCare => CharacterAttributesCompetences.MedicalCare > 0,
                    _ => false,
                };
        }

        public void DecreaseCompetence(Competences? competence)
        {
            switch (competence)
            {
                case Competences.HeavyMachines:
                    CharacterAttributesCompetences.HeavyMachines--;
                    break;
                case Competences.Stamina:
                    CharacterAttributesCompetences.Stamina--;
                    break;
                case Competences.CloseCombat:
                    CharacterAttributesCompetences.CloseCombat--;
                    break;
                case Competences.Mobility:
                    CharacterAttributesCompetences.Mobility--;
                    break;
                case Competences.Piloting:
                    CharacterAttributesCompetences.Piloting--;
                    break;
                case Competences.RangeCombat:
                    CharacterAttributesCompetences.RangeCombat--;
                    break;
                case Competences.Observation:
                    CharacterAttributesCompetences.Observation--;
                    break;
                case Competences.Comtech:
                    CharacterAttributesCompetences.Comtech--;
                    break;
                case Competences.Survival:
                    CharacterAttributesCompetences.Survival--;
                    break;
                case Competences.Manipulation:
                    CharacterAttributesCompetences.Manipulation--;
                    break;
                case Competences.Commandment:
                    CharacterAttributesCompetences.Commandment--;
                    break;
                case Competences.MedicalCare:
                    CharacterAttributesCompetences.MedicalCare--;
                    break;
            }

            CompetencePoints++;
            IncreaseCompetenceCommand.RaiseCanExecuteChanged();
            DecreaseCompetenceCommand.RaiseCanExecuteChanged();
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

        private bool IsKeyCompetence(Competences? competence)
        {
            if (CharacterCreation is null || competence is null) return false;
            IEnumerable<string> characterCompetences = _characterService.GetCareer(CharacterCreation.Career).Competences;
            if (characterCompetences.Count() <= 0) return false;
            string competenceString = Global.Competences.GetValueOrDefault(competence);
            if (string.IsNullOrEmpty(competenceString)) return false;

            return characterCompetences.Contains(competenceString);
        }
    }
}
