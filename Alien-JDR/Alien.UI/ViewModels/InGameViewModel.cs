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
    public class InGameViewModel : ViewModelBase
    {
        public InGameViewModel(IRegionNavigationService regionNavigationService, IAuthenticator authenticator, IMapper mapper) 
            : base(regionNavigationService, authenticator, mapper)
        {

        }

        public string CPing(dynamic cli, string x, string y)
        {
            // TODO : Logique X
            return string.Empty;
        }
    }
}
