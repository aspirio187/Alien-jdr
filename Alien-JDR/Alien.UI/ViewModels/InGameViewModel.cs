using Alien.UI.States;
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
        public InGameViewModel(IRegionNavigationService regionNavigationService, IAuthenticator authenticator) 
            : base(regionNavigationService, authenticator)
        {

        }

        public string CPing(dynamic cli, string x, string y)
        {
            // TODO : Logique X
            return string.Empty;
        }
    }
}
