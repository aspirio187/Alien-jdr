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

        public CharacterCareerSelectionModel CareerSelection
        {
            get { return _careerSelection; }
            set { SetProperty(ref _careerSelection, value); }
        }

        public ObservableCollection<CareerModel> Careers { get; set; }

        private DelegateCommand _navigateNextPageCommand;

        public DelegateCommand NavigateNextPageCommand => _navigateNextPageCommand ??= new DelegateCommand(NavigateNextPage, CanNavigateNextPage);

        public CharacterCareerSelectionViewModel(IRegionNavigationService regionNavigationService, IAuthenticator authenticator, ICharacterService characterService)
            : base(regionNavigationService, authenticator)
        {
            _characterService = characterService ??
                throw new ArgumentNullException(nameof(characterService));

            LoadCareers();
        }

        public void NavigateNextPage()
        {
            CharacterCreationDto characterCreation = new CharacterCreationDto()
            {
                Career = CareerSelection.SelectedCareer.Name,
                Race = CareerSelection.Race.ConvertToString()
            };

            Navigate(ViewsEnum.CharacterInfosView, new Dictionary<string, object>()
            {
                { Global.CHARACTER_CREATION, characterCreation }
            });
        }

        public bool CanNavigateNextPage()
        {
            if (CareerSelection is null) return false;
            if (CareerSelection.SelectedCareer is null) return false;

            return true;
        }

        public bool PersistInHistory()
        {
            return true;
        }

        private void LoadCareers()
        {
            IEnumerable<CareerFromJsonDto> careersFromFile = _characterService.GetCareersFromJson();

            List<CareerModel> careers = new List<CareerModel>();

            foreach (var career in careersFromFile)
            {
                careers.Add(new CareerModel()
                {
                    Name = career.Name,
                    ImagePath = career.Image,
                    Description = career.Description
                });
            }

            Careers = new ObservableCollection<CareerModel>(careers);
        }
    }
}
