using Alien.BLL.Dtos;
using Alien.BLL.Interfaces;
using Alien.Tools.Helpers;
using Alien.UI.Commands;
using Alien.UI.Helpers;
using Alien.UI.Managers;
using Alien.UI.Models;
using Alien.UI.States;
using Alien.UI.Views;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Alien.UI.ViewModels
{
    public class CharacterAttributesCompetencesViewModel : ViewModelBase
    {
        private readonly ICharacterService _characterService;
        private readonly NavigationManager _navigationManager;

        public CharacterCreationDto CharacterCreation { get; set; } = new();

        private CharacterAttributesCompetencesModel _characterAttributesCompetences = new();

        public CharacterAttributesCompetencesModel CharacterAttributesCompetences
        {
            get { return _characterAttributesCompetences; }
            set
            {
                _characterAttributesCompetences = value;
                NotifyPropertyChanged();
            }
        }

        private int _attributePoints;

        public int AttributePoints
        {
            get { return _attributePoints; }
            set
            {
                _attributePoints = value;
                NotifyPropertyChanged();
                NavigateNextPageCommand.CanExecute(null);
            }
        }

        private int _competencePoints;

        public int CompetencePoints
        {
            get { return _competencePoints; }
            set
            {
                _competencePoints = value;
                NotifyPropertyChanged();
                NavigateNextPageCommand.CanExecute(null);
            }
        }

        public ICommand IncreaseAttributeCommand { get; private set; }
        public ICommand DecreaseAttributeCommand { get; private set; }
        public ICommand IncreaseCompetenceCommand { get; private set; }
        public ICommand DecreaseCompetenceCommand { get; private set; }
        public ICommand NavigateBackCommand { get; private set; }
        public ICommand NavigateNextPageCommand { get; private set; }

        public CharacterAttributesCompetencesViewModel(IAuthenticator authenticator, IMapper mapper, ICharacterService characterService, NavigationManager navigationManager)
            : base(authenticator, mapper)
        {
            if (characterService is null)
            {
                throw new ArgumentNullException(nameof(characterService));
            }

            if (navigationManager is null)
            {
                throw new ArgumentNullException(nameof(navigationManager));
            }

            _characterService = characterService;
            _navigationManager = navigationManager;

            AttributePoints = 6;
            CompetencePoints = 10;

            IncreaseAttributeCommand = new RelayCommand<Attributes?>(IncreaseAttribute, CanIncreaseAttributes);
            DecreaseAttributeCommand = new RelayCommand<Attributes?>(DecreaseAttribute, CanDecreaseAttribute);
            IncreaseCompetenceCommand = new RelayCommand<Competences?>(IncreaseCompetence, CanIncreaseCompetence);
            DecreaseCompetenceCommand = new RelayCommand<Competences?>(DecreaseCompetence, CanDecreaseCompetence);
            NavigateBackCommand = new RelayCommand(NavigateBack);
            NavigateNextPageCommand = new RelayCommand(NavigateNextPage, CanNavigateNextPage);

            IncreaseAttributeCommand.CanExecute(null);
            DecreaseAttributeCommand.CanExecute(null);
            IncreaseCompetenceCommand.CanExecute(null);
            DecreaseCompetenceCommand.CanExecute(null);
            NavigateNextPageCommand.CanExecute(null);
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
            IncreaseAttributeCommand.CanExecute(null);
            DecreaseAttributeCommand.CanExecute(null);
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
            IncreaseAttributeCommand.CanExecute(null);
            DecreaseAttributeCommand.CanExecute(null);
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
            IncreaseCompetenceCommand.CanExecute(null);
            DecreaseCompetenceCommand.CanExecute(null);
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
            IncreaseCompetenceCommand.CanExecute(null);
            DecreaseCompetenceCommand.CanExecute(null);
        }

        public void NavigateBack()
        {
            if (_navigationManager.CanNavigateBack())
            {
                _navigationManager.NavigateBack();
            }
        }

        public bool CanNavigateNextPage()
        {
            return AttributePoints == 0 && CompetencePoints == 0 && CharacterAttributesCompetences.IsValid;
        }

        public void NavigateNextPage()
        {
            CharacterCreation.Strength = CharacterAttributesCompetences.Strength;
            CharacterCreation.Agility = CharacterAttributesCompetences.Agility;
            CharacterCreation.Mind = CharacterAttributesCompetences.Mind;
            CharacterCreation.Empathy = CharacterAttributesCompetences.Empathy;

            CharacterCreation.HeavyMachines = CharacterAttributesCompetences.HeavyMachines;
            CharacterCreation.Stamina = CharacterAttributesCompetences.Stamina;
            CharacterCreation.CloseCombat = CharacterAttributesCompetences.CloseCombat;
            CharacterCreation.Mobility = CharacterAttributesCompetences.Mobility;
            CharacterCreation.Piloting = CharacterAttributesCompetences.Piloting;
            CharacterCreation.RangedCombat = CharacterAttributesCompetences.RangeCombat;
            CharacterCreation.Observation = CharacterAttributesCompetences.Observation;
            CharacterCreation.Comtech = CharacterAttributesCompetences.Comtech;
            CharacterCreation.Survival = CharacterAttributesCompetences.Survival;
            CharacterCreation.Manipulation = CharacterAttributesCompetences.Manipulation;
            CharacterCreation.Commandment = CharacterAttributesCompetences.Commandment;
            CharacterCreation.MedicalCare = CharacterAttributesCompetences.MedicalCare;

            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { Global.CHARACTER_CREATION, CharacterCreation }
            };

            if (CharacterCreation.Race.Equals(RaceEnum.Android.ToString()))
            {
                _navigationManager.Navigate(nameof(CharacterAndroidCreationView), parameters: parameters);
            }
            else
            {
                _navigationManager.Navigate(nameof(CharacterCreationSummaryView), parameters: parameters);
            }
        }

        //public override void OnNavigatedTo(NavigationContext navigationContext)
        //{
        //    base.OnNavigatedTo(navigationContext);

        //    CharacterCreation = navigationContext.Parameters.GetValue<CharacterCreationDto>(Global.CHARACTER_CREATION);
        //    IncreaseAttributeCommand.RaiseCanExecuteChanged();
        //}

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
