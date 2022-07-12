using Alien.BLL.Dtos;
using Alien.BLL.Interfaces;
using Alien.UI.Helpers;
using Alien.UI.Models;
using Alien.UI.States;
using AutoMapper;
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
    public class CharacterPublicInfosViewModel : ViewModelBase, IJournalAware
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
            set
            {
                if(value == RaceEnum.Humain)
                {
                    if(PublicCharacter is not null && SelectedAttributes is not null && SelectedAttributes.Any(a => a))
                    {
                        if (SelectedAttributes[(int)Attributes.Force])
                        {
                            PublicCharacter.Strength -= 3;
                            SelectedAttributes[(int)Attributes.Force] = false;
                        }
                        if (SelectedAttributes[(int)Attributes.Agilité])
                        {
                            PublicCharacter.Agility -= 3;
                            SelectedAttributes[(int)Attributes.Agilité] = false;
                        }
                        if (SelectedAttributes[(int)Attributes.Esprit])
                        {
                            PublicCharacter.Mind -= 3;
                            SelectedAttributes[(int)Attributes.Esprit] = false;
                        }
                        if (SelectedAttributes[(int)Attributes.Empathie])
                        {
                            PublicCharacter.Empathy -= 3;
                            SelectedAttributes[(int)Attributes.Empathie] = false;
                        }
                    }
                }

                SetProperty(ref _race, value);
                SelectAttributeCommand?.RaiseCanExecuteChanged();
            }
        }
        private ObservableCollection<bool> _selectedAttributes;

        public ObservableCollection<bool> SelectedAttributes
        {
            get { return _selectedAttributes; }
            set { SetProperty(ref _selectedAttributes, value); }
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
        private DelegateCommand<Attributes?> _selectAttributeCommand;
        private DelegateCommand<Competences?> _increaseCompetenceCommand;
        private DelegateCommand<Competences?> _decreaseCompetenceCommand;
        private DelegateCommand _navigateBackCommand;
        private DelegateCommand _createPublicCharacterCommand;


        public DelegateCommand<Attributes?> IncreaseAttributeCommand => _increaseAttributeCommand ??= new DelegateCommand<Attributes?>(IncreaseAttribute, CanIncreaseAttributes);
        public DelegateCommand<Attributes?> DecreaseAttributeCommand => _decreaseAttributeCommand ??= new DelegateCommand<Attributes?>(DecreaseAttribute, CanDecreaseAttribute);
        public DelegateCommand<Attributes?> SelectAttributeCommand => _selectAttributeCommand ??= new DelegateCommand<Attributes?>(SelectAttribute, CanSelectAttribute);
        public DelegateCommand<Competences?> IncreaseCompetenceCommand => _increaseCompetenceCommand ??= new DelegateCommand<Competences?>(IncreaseCompetence, CanIncreaseCompetence);
        public DelegateCommand<Competences?> DecreaseCompetenceCommand => _decreaseCompetenceCommand ??= new DelegateCommand<Competences?>(DecreaseCompetence, CanDecreaseCompetence);
        public DelegateCommand NavigateBackCommand => _navigateBackCommand ??= new(NavigateBack);
        public DelegateCommand CreatePublicCharacterCommand => _createPublicCharacterCommand ??= new(CreatePublicCharacter);

        public CharacterPublicInfosViewModel(IRegionNavigationService regionNavigationService, IAuthenticator authenticator, IMapper mapper, ICharacterService characterService)
            : base(regionNavigationService, authenticator, mapper)
        {
            _characterService = characterService ??
                throw new ArgumentNullException(nameof(characterService));

            IncreaseAttributeCommand.RaiseCanExecuteChanged();
            DecreaseAttributeCommand.RaiseCanExecuteChanged();
            SelectAttributeCommand.RaiseCanExecuteChanged();
            IncreaseCompetenceCommand.RaiseCanExecuteChanged();
            DecreaseCompetenceCommand.RaiseCanExecuteChanged();
        }

        public bool CanIncreaseAttributes(Attributes? attribute)
        {
            if (attribute is null) return false;
            if (AttributePoints <= 0) return false;

            switch (attribute)
            {
                case Attributes.Force:
                    if (IsKeyAttribute(attribute))
                    {
                        if (SelectedAttributes[(int)attribute]) return PublicCharacter.Strength < (3 + 5);
                        else return PublicCharacter.Strength < 5;
                    }
                    else
                    {
                        if (SelectedAttributes[(int)attribute]) return PublicCharacter.Strength < (3 + 4);
                        else return PublicCharacter.Strength < 4;
                    }
                case Attributes.Agilité:
                    if (IsKeyAttribute(attribute))
                    {
                        if (SelectedAttributes[(int)attribute]) return PublicCharacter.Agility < (3 + 5);
                        else return PublicCharacter.Agility < 5;
                    }
                    else
                    {
                        if (SelectedAttributes[(int)attribute]) return PublicCharacter.Agility < (3 + 4);
                        else return PublicCharacter.Agility < 4;
                    }
                case Attributes.Esprit:
                    if (IsKeyAttribute(attribute))
                    {
                        if (SelectedAttributes[(int)attribute]) return PublicCharacter.Mind < (3 + 5);
                        else return PublicCharacter.Mind < 5;
                    }
                    else
                    {
                        if (SelectedAttributes[(int)attribute]) return PublicCharacter.Mind < (3 + 4);
                        else return PublicCharacter.Mind < 4;
                    }
                case Attributes.Empathie:
                    if (IsKeyAttribute(attribute))
                    {
                        return SelectedAttributes[(int)attribute] ? PublicCharacter.Empathy < (3 + 5) : PublicCharacter.Empathy < 5;
                    }
                    else
                    {
                        return SelectedAttributes[(int)attribute] ? PublicCharacter.Empathy < (3 + 4) : PublicCharacter.Empathy < 4;
                    }
                default:
                    return false;
            }
        }

        public void IncreaseAttribute(Attributes? attribute)
        {
            switch (attribute)
            {
                case Attributes.Force:
                    PublicCharacter.Strength += 1;
                    break;
                case Attributes.Agilité:
                    PublicCharacter.Agility += 1;
                    break;
                case Attributes.Esprit:
                    PublicCharacter.Mind += 1;
                    break;
                case Attributes.Empathie:
                    PublicCharacter.Empathy += 1;
                    break;
                default: break;
            }

            AttributePoints--;
            IncreaseAttributeCommand.RaiseCanExecuteChanged();
            DecreaseAttributeCommand.RaiseCanExecuteChanged();
        }

        public bool CanDecreaseAttribute(Attributes? attribute)
        {
            return attribute is null
                ? false 
                : AttributePoints >= 6
                ? false
                : attribute switch
                {
                    Attributes.Force => PublicCharacter.Strength > 2,
                    Attributes.Agilité => PublicCharacter.Agility > 2,
                    Attributes.Esprit => PublicCharacter.Mind > 2,
                    Attributes.Empathie => PublicCharacter.Empathy > 2,
                    _ => false,
                };
        }

        public void DecreaseAttribute(Attributes? attribute)
        {
            switch (attribute)
            {
                case Attributes.Force:
                    PublicCharacter.Strength -= 1;
                    break;
                case Attributes.Agilité:
                    PublicCharacter.Agility -= 1;
                    break;
                case Attributes.Esprit:
                    PublicCharacter.Mind -= 1;
                    break;
                case Attributes.Empathie:
                    PublicCharacter.Empathy -= 1;
                    break;
            }

            AttributePoints++;
            IncreaseAttributeCommand.RaiseCanExecuteChanged();
            DecreaseAttributeCommand.RaiseCanExecuteChanged();
        }

        public bool CanSelectAttribute(Attributes? attribute)
        {
            return SelectedAttributes is not null && 
                Race.Equals(RaceEnum.Android) && 
                attribute is not null && 
                (SelectedAttributes.Count(a => a) < 2 || SelectedAttributes[(int)attribute]);
        }

        public void SelectAttribute(Attributes? attribute)
        {
            SelectedAttributes[(int)attribute] = !SelectedAttributes[(int)attribute];

            switch (attribute)
            {
                case Attributes.Force:
                    if (SelectedAttributes[(int)attribute])
                    {
                        PublicCharacter.Strength += 3;
                    }
                    else
                    {
                        PublicCharacter.Strength -= 3;
                    }
                    break;
                case Attributes.Agilité:
                    if (SelectedAttributes[(int)attribute])
                    {
                        PublicCharacter.Agility += 3;
                    }
                    else
                    {
                        PublicCharacter.Agility -= 3;
                    }
                    break;
                case Attributes.Esprit:
                    if (SelectedAttributes[(int)attribute])
                    {
                        PublicCharacter.Mind += 3;
                    }
                    else
                    {
                        PublicCharacter.Mind -= 3;
                    }
                    break;
                case Attributes.Empathie:
                    if (SelectedAttributes[(int)attribute])
                    {
                        PublicCharacter.Empathy += 3;
                    }
                    else
                    {
                        PublicCharacter.Empathy -= 3;
                    }
                    break;
            }

            CreatePublicCharacterCommand.RaiseCanExecuteChanged();
            SelectAttributeCommand.RaiseCanExecuteChanged();
        }

        public bool CanIncreaseCompetence(Competences? competence)
        {
            return competence is null || CompetencePoints <= 0
                ? false
                : competence switch
                {
                    Competences.HeavyMachines => IsKeyCompetence(competence) ? PublicCharacter.HeavyMachines < 3 : PublicCharacter.HeavyMachines < 1,
                    Competences.Stamina => IsKeyCompetence(competence) ? PublicCharacter.Stamina < 3 : PublicCharacter.Stamina < 1,
                    Competences.CloseCombat => IsKeyCompetence(competence) ? PublicCharacter.CloseCombat < 3 : PublicCharacter.CloseCombat < 1,
                    Competences.Mobility => IsKeyCompetence(competence) ? PublicCharacter.Mobility < 3 : PublicCharacter.Mobility < 1,
                    Competences.Piloting => IsKeyCompetence(competence) ? PublicCharacter.Piloting < 3 : PublicCharacter.Piloting < 1,
                    Competences.RangeCombat => IsKeyCompetence(competence) ? PublicCharacter.RangeCombat < 3 : PublicCharacter.RangeCombat < 1,
                    Competences.Observation => IsKeyCompetence(competence) ? PublicCharacter.Observation < 3 : PublicCharacter.Observation < 1,
                    Competences.Comtech => IsKeyCompetence(competence) ? PublicCharacter.Comtech < 3 : PublicCharacter.Comtech < 1,
                    Competences.Survival => IsKeyCompetence(competence) ? PublicCharacter.Survival < 3 : PublicCharacter.Survival < 1,
                    Competences.Manipulation => IsKeyCompetence(competence) ? PublicCharacter.Manipulation < 3 : PublicCharacter.Manipulation < 1,
                    Competences.Commandment => IsKeyCompetence(competence) ? PublicCharacter.Commandment < 3 : PublicCharacter.Commandment < 1,
                    Competences.MedicalCare => IsKeyCompetence(competence) ? PublicCharacter.MedicalCare < 3 : PublicCharacter.MedicalCare < 1,
                    _ => false,
                };
        }

        public void IncreaseCompetence(Competences? competence)
        {
            switch (competence)
            {
                case Competences.HeavyMachines:
                    PublicCharacter.HeavyMachines++;
                    break;
                case Competences.Stamina:
                    PublicCharacter.Stamina++;
                    break;
                case Competences.CloseCombat:
                    PublicCharacter.CloseCombat++;
                    break;
                case Competences.Mobility:
                    PublicCharacter.Mobility++;
                    break;
                case Competences.Piloting:
                    PublicCharacter.Piloting++;
                    break;
                case Competences.RangeCombat:
                    PublicCharacter.RangeCombat++;
                    break;
                case Competences.Observation:
                    PublicCharacter.Observation++;
                    break;
                case Competences.Comtech:
                    PublicCharacter.Comtech++;
                    break;
                case Competences.Survival:
                    PublicCharacter.Survival++;
                    break;
                case Competences.Manipulation:
                    PublicCharacter.Manipulation++;
                    break;
                case Competences.Commandment:
                    PublicCharacter.Commandment++;
                    break;
                case Competences.MedicalCare:
                    PublicCharacter.MedicalCare++;
                    break;
            }

            CompetencePoints--;
            IncreaseCompetenceCommand.RaiseCanExecuteChanged();
            DecreaseCompetenceCommand.RaiseCanExecuteChanged();
        }

        public bool CanDecreaseCompetence(Competences? competence)
        {
            return competence is null || CompetencePoints >= 10
                ? false
                : competence switch
                {
                    Competences.HeavyMachines => PublicCharacter.HeavyMachines > 0,
                    Competences.Stamina => PublicCharacter.Stamina > 0,
                    Competences.CloseCombat => PublicCharacter.CloseCombat > 0,
                    Competences.Mobility => PublicCharacter.Mobility > 0,
                    Competences.Piloting => PublicCharacter.Piloting > 0,
                    Competences.RangeCombat => PublicCharacter.RangeCombat > 0,
                    Competences.Observation => PublicCharacter.Observation > 0,
                    Competences.Comtech => PublicCharacter.Comtech > 0,
                    Competences.Survival => PublicCharacter.Survival > 0,
                    Competences.Manipulation => PublicCharacter.Manipulation > 0,
                    Competences.Commandment => PublicCharacter.Commandment > 0,
                    Competences.MedicalCare => PublicCharacter.MedicalCare > 0,
                    _ => false,
                };
        }

        public void DecreaseCompetence(Competences? competence)
        {
            switch (competence)
            {
                case Competences.HeavyMachines:
                    PublicCharacter.HeavyMachines--;
                    break;
                case Competences.Stamina:
                    PublicCharacter.Stamina--;
                    break;
                case Competences.CloseCombat:
                    PublicCharacter.CloseCombat--;
                    break;
                case Competences.Mobility:
                    PublicCharacter.Mobility--;
                    break;
                case Competences.Piloting:
                    PublicCharacter.Piloting--;
                    break;
                case Competences.RangeCombat:
                    PublicCharacter.RangeCombat--;
                    break;
                case Competences.Observation:
                    PublicCharacter.Observation--;
                    break;
                case Competences.Comtech:
                    PublicCharacter.Comtech--;
                    break;
                case Competences.Survival:
                    PublicCharacter.Survival--;
                    break;
                case Competences.Manipulation:
                    PublicCharacter.Manipulation--;
                    break;
                case Competences.Commandment:
                    PublicCharacter.Commandment--;
                    break;
                case Competences.MedicalCare:
                    PublicCharacter.MedicalCare--;
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

        public bool CanCreatePublicCharacter()
        {
            if (AttributePoints > 0) return false;
            if (CompetencePoints > 0) return false;
            if (!PublicCharacter.IsValid) return false;
            return true;
        }

        public async void CreatePublicCharacter()
        {
            if (PublicCharacter.IsValid)
            {
                CharacterCreation.Race = Race.ToString();
                CharacterCreation.Objectives = PublicCharacter.Objectives;
                CharacterCreation.Friends = PublicCharacter.Friends;

                CharacterCreation.Strength = PublicCharacter.Strength;
                CharacterCreation.Agility = PublicCharacter.Agility;
                CharacterCreation.Empathy = PublicCharacter.Mind;
                CharacterCreation.Empathy = PublicCharacter.Empathy;

                CharacterCreation.HeavyMachines = PublicCharacter.HeavyMachines;
                CharacterCreation.Stamina = PublicCharacter.Stamina;
                CharacterCreation.CloseCombat = PublicCharacter.CloseCombat;
                CharacterCreation.Mobility = PublicCharacter.Mobility;
                CharacterCreation.Piloting = PublicCharacter.Piloting;
                CharacterCreation.RangedCombat = PublicCharacter.RangeCombat;
                CharacterCreation.Observation = PublicCharacter.Observation;
                CharacterCreation.Comtech = PublicCharacter.Comtech;
                CharacterCreation.Survival = PublicCharacter.Survival;
                CharacterCreation.Manipulation = PublicCharacter.Manipulation;
                CharacterCreation.Commandment = PublicCharacter.Commandment;
                CharacterCreation.MedicalCare = PublicCharacter.MedicalCare;

                if (await _characterService.CreateCharacter(CharacterCreation, _authenticator.User.Id))
                {
                    _regionNavigationService.Journal.Clear();
                    Navigate(ViewsEnum.CharactersView);
                }
            }
        }

        public override void OnNavigatedFrom(NavigationContext navigationContext)
        {
            base.OnNavigatedFrom(navigationContext);

            //_characterService.DeleteCharacter(CharacterCreation.IdentificationStamp);   
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            CharacterCreation = navigationContext.Parameters.GetValue<CharacterCreationDto>(Global.CHARACTER_CREATION);

            PublicCharacter.Objectives = CharacterCreation.Objectives;
            PublicCharacter.Friends = CharacterCreation.Friends;
            PublicCharacter.Rivals = CharacterCreation.Rivals;


            Race = (RaceEnum)Enum.Parse(typeof(RaceEnum), CharacterCreation.Race);

            PublicCharacter.Strength = CharacterCreation.Strength;
            PublicCharacter.Agility = CharacterCreation.Agility;
            PublicCharacter.Mind = CharacterCreation.Mind;
            PublicCharacter.Empathy = CharacterCreation.Empathy;

            PublicCharacter.HeavyMachines = CharacterCreation.HeavyMachines;
            PublicCharacter.Stamina = CharacterCreation.Stamina;
            PublicCharacter.CloseCombat = CharacterCreation.CloseCombat;
            PublicCharacter.Mobility = CharacterCreation.Mobility;
            PublicCharacter.Piloting = CharacterCreation.Piloting;
            PublicCharacter.RangeCombat = CharacterCreation.RangedCombat;
            PublicCharacter.Observation = CharacterCreation.Observation;
            PublicCharacter.Comtech = CharacterCreation.Comtech;
            PublicCharacter.Survival = CharacterCreation.Survival;
            PublicCharacter.Manipulation = CharacterCreation.Manipulation;
            PublicCharacter.Commandment = CharacterCreation.Commandment;
            PublicCharacter.MedicalCare = CharacterCreation.MedicalCare;

            SelectedAttributes = new(CharacterCreation.SelectedAttributes);

            IncreaseAttributeCommand?.RaiseCanExecuteChanged();
            DecreaseAttributeCommand?.RaiseCanExecuteChanged();
            SelectAttributeCommand?.RaiseCanExecuteChanged();
            IncreaseCompetenceCommand?.RaiseCanExecuteChanged();
            DecreaseCompetenceCommand?.RaiseCanExecuteChanged();
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
