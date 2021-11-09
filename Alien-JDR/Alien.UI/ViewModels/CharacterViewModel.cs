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
    public class CharacterViewModel : ViewModelBase
    {
        private DelegateCommand _navigateCreateCharacterCommand;

        public DelegateCommand NavigateCreateCharacterCommand => _navigateCreateCharacterCommand ??= new DelegateCommand(NavigateCreateCharacter);

        public CharacterViewModel(IRegionNavigationService regionNavigationService, IAuthenticator authenticator)
            : base(regionNavigationService, authenticator)
        {

        }

        public void NavigateCreateCharacter()
        {

        }
    }
}
