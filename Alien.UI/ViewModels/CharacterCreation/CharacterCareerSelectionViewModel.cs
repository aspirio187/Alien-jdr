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
    public class CharacterCareerSelectionViewModel : ViewModelBase, IJournalAware
    {
        private readonly ICharacterService _characterService;

        private CharacterCareerSelectionModel _selectedCareer;

        public CharacterCareerSelectionModel SelectedCareer
        {
            get { return _selectedCareer; }
            set
            {
                SetProperty(ref _selectedCareer, value);
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

        public CharacterCareerSelectionViewModel(IRegionNavigationService regionNavigationService, IAuthenticator authenticator, IMapper mapper, ICharacterService characterService)
            : base(regionNavigationService, authenticator, mapper)
        {
            _characterService = characterService ??
                throw new ArgumentNullException(nameof(characterService));

            LoadCareers();

            NavigateNextPageCommand.RaiseCanExecuteChanged();
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
