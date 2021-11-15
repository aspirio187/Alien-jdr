using Alien.UI.Models;
using Alien.UI.States;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.UI.ViewModels
{
    public class CharacterCareerSelectionViewModel : ViewModelBase, IJournalAware
    {
        private CharacterCareerSelectionModel _careerSelection;

        public CharacterCareerSelectionModel CareerSelection
        {
            get { return _careerSelection; }
            set { SetProperty(ref _careerSelection, value); }
        }



        public CharacterCareerSelectionViewModel(IRegionNavigationService regionNavigationService, IAuthenticator authenticator)
            : base(regionNavigationService, authenticator)
        {
            // test
        }

        public bool PersistInHistory()
        {
            return true;
        }
    }
}
