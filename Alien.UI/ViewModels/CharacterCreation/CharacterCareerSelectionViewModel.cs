using Alien.BLL.Dtos;
using Alien.BLL.Interfaces;
using Alien.UI.Commands;
using Alien.UI.Helpers;
using Alien.UI.Managers;
using Alien.UI.Models;
using Alien.UI.States;
using Alien.UI.Views;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Alien.UI.ViewModels
{
    public class CharacterCareerSelectionViewModel : ViewModelBase
    {
        private readonly ICharacterService _characterService;
        private readonly NavigationManager _navigationManager;

        private CharacterCareerSelectionModel? _selectedCareer;

        public CharacterCareerSelectionModel? SelectedCareer
        {
            get { return _selectedCareer; }
            set
            {
                _selectedCareer = value;
                NotifyPropertyChanged();
            }
        }

        private RaceEnum _selectedRace;

        public RaceEnum SelectedRace
        {
            get { return _selectedRace; }
            set
            {
                _selectedRace = value;
                NotifyPropertyChanged();
            }
        }


        public ObservableCollection<CharacterCareerSelectionModel> Careers { get; set; } = new();

        public ICommand NavigateNextPageCommand { get; private set; }

        public CharacterCareerSelectionViewModel(IAuthenticator authenticator, IMapper mapper, ICharacterService characterService, NavigationManager navigationManager)
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

            LoadCareers();

            NavigateNextPageCommand = new RelayCommand(NavigateNextPage, CanNavigateNextPage);
            _navigationManager = navigationManager;
        }

        public bool CanNavigateNextPage()
        {
            return SelectedCareer is not null && (SelectedCareer.IsValid);
        }

        public void NavigateNextPage()
        {
            CharacterCreationDto characterCreation = new()
            {
                Image = SelectedCareer.ImagePath,
                Career = SelectedCareer.Name,
                Race = SelectedRace.ToString(),
                IdentificationStamp = Guid.NewGuid()
            };

            _navigationManager.Navigate(nameof(CharacterInfosCreationView), parameters: new Dictionary<string, object>()
            {
                { Global.CHARACTER_CREATION, characterCreation }
            });
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
