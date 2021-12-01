using Alien.BLL.Dtos;
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
    public class CharacterAndroidCreationViewModel : ViewModelBase, IJournalAware
    {
        public CharacterCreationDto CharacterCreation { get; set; }

        public ObservableCollection<CharacterAndroidCreationModel> CharacterAndroids { get; set; } = new();

        private DelegateCommand _navigateBackCommand;
        private DelegateCommand _navigateNextPageCommand;

        public DelegateCommand NavigateBackCommand => _navigateBackCommand ??= new(NavigateBack);
        public DelegateCommand NavigateNextPageCommand => _navigateNextPageCommand ??= new(NavigateNextPage);

        public CharacterAndroidCreationViewModel(IRegionNavigationService regionNavigationService, IAuthenticator authenticator)
            : base(regionNavigationService, authenticator)
        {

        }

        public void NavigateBack()
        {
            if (_regionNavigationService.Journal.CanGoBack)
            {
                _regionNavigationService.Journal.GoBack();
            }
        }

        public bool CanSelectAttribute(Attributes? attribute)
        {
            if (attribute is null) return false;
            return false;
        }

        public void SelectAttribute(Attributes? attribute)
        {

        }

        public bool CanNavigateNextPage()
        {
            return CharacterAndroids.Count(c => c.IsSelected) == 2;
        }

        public void NavigateNextPage()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { Global.CHARACTER_CREATION, CharacterCreation }
            };

            Navigate(ViewsEnum.CharacterCreationSummaryView, parameters);
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
    }
}
