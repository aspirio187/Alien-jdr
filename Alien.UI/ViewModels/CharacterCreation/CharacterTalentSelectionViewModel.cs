using Alien.BLL.Dtos;
using Alien.BLL.Interfaces;
using Alien.UI.Commands;
using Alien.UI.Helpers;
using Alien.UI.Managers;
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
    public class CharacterTalentSelectionViewModel : ViewModelBase
    {
        private readonly ICharacterService _characterService;
        private readonly NavigationManager _navigationManager;

        public CharacterCreationDto CharacterCreation { get; private set; } = new();

        private ObservableCollection<TalentFromJsonDto> _talents = new();

        public ObservableCollection<TalentFromJsonDto> Talents
        {
            get { return _talents; }
            set
            {
                _talents = value;
                NotifyPropertyChanged();
            }
        }

        private TalentFromJsonDto _selectedTalent = new();

        public TalentFromJsonDto SelectedTalent
        {
            get { return _selectedTalent; }
            set
            {
                _selectedTalent = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand NavigateBackCommand { get; private set; }
        public ICommand NavigateNextPageCommand { get; private set; }

        public CharacterTalentSelectionViewModel(IAuthenticator authenticator, IMapper mapper, ICharacterService characterService, NavigationManager navigationManager)
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

            NavigateBackCommand = new RelayCommand(NavigateBack);
            NavigateNextPageCommand = new RelayCommand(NavigateNextPage);
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
            return SelectedTalent is not null && !string.IsNullOrEmpty(SelectedTalent.Name);
        }

        public void NavigateNextPage()
        {
            CharacterCreation.Talent = SelectedTalent.Name;

            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { Global.CHARACTER_CREATION, CharacterCreation }
            };

            _navigationManager.Navigate(nameof(CharacterAttributesCompetencesView), parameters: parameters);
        }

        //public override void OnNavigatedTo(NavigationContext navigationContext)
        //{
        //    base.OnNavigatedTo(navigationContext);

        //    CharacterCreation = navigationContext.Parameters.GetValue<CharacterCreationDto>(Global.CHARACTER_CREATION);
        //    Talents = new(_characterService.GetTalentsFromJson(CharacterCreation.Career));
        //}
    }
}
