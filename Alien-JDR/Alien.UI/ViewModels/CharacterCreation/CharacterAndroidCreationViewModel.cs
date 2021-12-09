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

        public ObservableCollection<bool> SelectedAttributes { get; set; } = new()
        {
            false,
            false,
            false,
            false
        };

        private DelegateCommand _navigateBackCommand;
        private DelegateCommand _navigateNextPageCommand;
        private DelegateCommand<Attributes?> _selectAttributeCommand;

        public DelegateCommand NavigateBackCommand => _navigateBackCommand ??= new(NavigateBack);
        public DelegateCommand NavigateNextPageCommand => _navigateNextPageCommand ??= new(NavigateNextPage, CanNavigateNextPage);
        public DelegateCommand<Attributes?> SelectAttributeCommand => _selectAttributeCommand ??= new DelegateCommand<Attributes?>(SelectAttribute, CanSelectAttribute);

        public CharacterAndroidCreationViewModel(IRegionNavigationService regionNavigationService, IAuthenticator authenticator)
            : base(regionNavigationService, authenticator)
        {
            SelectAttributeCommand.RaiseCanExecuteChanged();
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
            return attribute is not null && (SelectedAttributes.Count(a => a) < 2 || SelectedAttributes[(int)attribute]);
        }

        public void SelectAttribute(Attributes? attribute)
        {
            SelectedAttributes[(int)attribute] = !SelectedAttributes[(int)attribute];
            NavigateNextPageCommand.RaiseCanExecuteChanged();
            SelectAttributeCommand.RaiseCanExecuteChanged();
        }

        public bool CanNavigateNextPage()
        {
            return SelectedAttributes.Count(a => a) == 2;
        }

        public void NavigateNextPage()
        {
            if (SelectedAttributes[0]) CharacterCreation.Strength += 3;
            if (SelectedAttributes[1]) CharacterCreation.Agility += 3;
            if (SelectedAttributes[2]) CharacterCreation.Mind += 3;
            if (SelectedAttributes[3]) CharacterCreation.Empathy += 3;

            CharacterCreation.SelectedAttributes = SelectedAttributes.ToArray();

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
