using Alien.UI.States;
using AutoMapper;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.UI.ViewModels
{
    public class CharacterSheetViewModel : ViewModelBase
    {
        public CharacterSheetViewModel(IRegionNavigationService regionNavigationService, IAuthenticator authenticator, IMapper mapper)
            : base(regionNavigationService, authenticator, mapper)
        {
        }
    }
}
