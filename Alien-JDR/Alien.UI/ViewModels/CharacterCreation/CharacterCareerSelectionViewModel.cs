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
    public class CharacterCareerSelectionViewModel : ViewModelBase, IJournalAware
    {
        private readonly ICharacterService _characterService;

        private CharacterCareerSelectionModel _careerSelection = new();

        public CharacterCareerSelectionModel SelectedCareer
        {
            get { return _careerSelection; }
            set
            {
                SetProperty(ref _careerSelection, value);
                NavigateNextPageCommand.RaiseCanExecuteChanged();
            }
        }

        private RaceEnum _selectedRace;

        public RaceEnum SelectedRace
        {
            get { return _selectedRace; }
            set
            {
                SetProperty(ref _selectedRace, value);
                NavigateNextPageCommand.RaiseCanExecuteChanged();
            }
        }


        public ObservableCollection<CharacterCareerSelectionModel> Careers { get; set; }

        private DelegateCommand _navigateNextPageCommand;

        public DelegateCommand NavigateNextPageCommand => _navigateNextPageCommand ??= new DelegateCommand(NavigateNextPage, CanNavigateNextPage);

        public CharacterCareerSelectionViewModel(IRegionNavigationService regionNavigationService, IAuthenticator authenticator, ICharacterService characterService)
            : base(regionNavigationService, authenticator)
        {
            _characterService = characterService ??
                throw new ArgumentNullException(nameof(characterService));

            LoadCareers();
        }

        public bool CanNavigateNextPage()
        {
            return SelectedCareer is null ? false : SelectedCareer.IsValid ? true : false;
        }

        public void NavigateNextPage()
        {
            CharacterCreationDto characterCreation = new()
            {
                Career = SelectedCareer.Name,
                Race = SelectedRace.ToString()
            };

            Navigate(ViewsEnum.CharacterInfosCreationView, new Dictionary<string, object>()
            {
                { Global.CHARACTER_CREATION, characterCreation }
            });
        }

        public bool PersistInHistory()
        {
            return true;
        }

        private void LoadCareers()
        {
            CareerFromJsonDto[] careersFromFile = _characterService.GetCareersFromJson();

            List<CharacterCareerSelectionModel> careers = new();

            foreach (CareerFromJsonDto career in careersFromFile)
            {
                careers.Add(new CharacterCareerSelectionModel()
                {
                    Name = career.Name,
                    ImagePath = career.Image,
                    Description = career.Description
                });
            }

            Careers = new ObservableCollection<CharacterCareerSelectionModel>(careers);
        }
    }
}
