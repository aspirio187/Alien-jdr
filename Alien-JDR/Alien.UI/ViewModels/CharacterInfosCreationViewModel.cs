using Alien.BLL.Dtos;
using Alien.UI.Helpers;
using Alien.UI.Models;
using Alien.UI.States;
using Prism.Commands;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.UI.ViewModels
{
    public class CharacterInfosCreationViewModel : ViewModelBase, IJournalAware
    {
        public CharacterCreationDto CharacterCreation { get; private set; }

        private CharacterInfosCreationModel _characterInfos;

        private DelegateCommand _navigateBackCommand;
        private DelegateCommand _navigateNextPageCommand;

        public CharacterInfosCreationModel CharacterInfos
        {
            get { return _characterInfos; }
            set { SetProperty(ref _characterInfos, value); }
        }

        public DelegateCommand NavigateBackCommand => _navigateBackCommand ??= new DelegateCommand(NavigateBack);
        public DelegateCommand NavigateNextPageCommand => _navigateNextPageCommand ??= new DelegateCommand(NavigateNextPage);

        public CharacterInfosCreationViewModel(IRegionNavigationService regionNavigationService, IAuthenticator authenticator)
            : base(regionNavigationService, authenticator)
        {

        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            CharacterCreation = navigationContext.Parameters.GetValue<CharacterCreationDto>(Global.CHARACTER_CREATION);
        }

        public void NavigateBack()
        {
            if (_regionNavigationService.Journal.CanGoBack)
            {
                _regionNavigationService.Journal.GoBack();
            }
        }

        public void NavigateNextPage()
        {
            if (CharacterInfos is null) return;

        }

        public bool PersistInHistory()
        {
            return true;
        }
    }
}