using Alien.BLL.Dtos;
using Alien.UI.Helpers;
using Alien.UI.States;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.UI.ViewModels
{
    public class CharacterAttributAndCompetenceViewModel : ViewModelBase, IJournalAware
    {
        public CharacterCreationDto CharacterCreation { get; set; }

        public CharacterAttributAndCompetenceViewModel(IRegionNavigationService regionNavigationService, IAuthenticator authenticator)
            : base(regionNavigationService, authenticator)
        {
        }

        public void NavigateNextPage()
        {
            if(CharacterCreation.Race.Equals(RaceEnum.Android.ToString()))
            {
                // TODO : Navigue vers la page pour les android
            }
            else
            {
                // TODO : Navigue vers la page pour récapitulative
            }
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
