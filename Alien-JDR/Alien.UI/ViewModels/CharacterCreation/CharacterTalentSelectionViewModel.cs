using Alien.BLL.Dtos;
using Alien.BLL.Interfaces;
using Alien.UI.Helpers;
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
    public class CharacterTalentSelectionViewModel : ViewModelBase, IJournalAware
    {
        private readonly ICharacterService _characterService;

        public CharacterCreationDto CharacterCreation { get; private set; }

        private ObservableCollection<TalentFromJsonDto> _talents;

        public ObservableCollection<TalentFromJsonDto> Talents
        {
            get { return _talents; }
            set { SetProperty(ref _talents, value); }
        }

        private TalentFromJsonDto _selectedTalent;

        public TalentFromJsonDto SelectedTalent
        {
            get { return _selectedTalent; }
            set
            {
                SetProperty(ref _selectedTalent, value);
                NavigateNextPageCommand.RaiseCanExecuteChanged();
            }
        }

        private DelegateCommand _navigateBackCommand;
        private DelegateCommand _navigateNextPageCommand;

        public DelegateCommand NavigateBackCommand => _navigateBackCommand ??= new(NavigateBack);
        public DelegateCommand NavigateNextPageCommand => _navigateNextPageCommand ??= new DelegateCommand(NavigateNextPage, CanNavigateNextPage);

        public CharacterTalentSelectionViewModel(IRegionNavigationService regionNavigationService, IAuthenticator authenticator, ICharacterService characterService)
            : base(regionNavigationService, authenticator)
        {
            _characterService = characterService ??
                throw new ArgumentNullException(nameof(characterService));
        }

        public void NavigateBack()
        {
            if (_regionNavigationService.Journal.CanGoBack)
            {
                _regionNavigationService.Journal.GoBack();
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

            Navigate(ViewsEnum.CharacterAttributesCompetencesView, parameters);
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            CharacterCreation = navigationContext.Parameters.GetValue<CharacterCreationDto>(Global.CHARACTER_CREATION);
            Talents = new(_characterService.GetTalentsFromJson(CharacterCreation.Career));
        }

        public bool PersistInHistory()
        {
            return true;
        }
    }
}
