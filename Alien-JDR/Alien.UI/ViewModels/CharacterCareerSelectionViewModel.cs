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
    public class CharacterCareerSelectionViewModel : ViewModelBase, IJournalAware
    {
        private CharacterCareerSelectionModel _careerSelection = new();

        public CharacterCareerSelectionModel CareerSelection
        {
            get { return _careerSelection; }
            set { SetProperty(ref _careerSelection, value); }
        }

        public ObservableCollection<CareerModel> Careers { get; set; }

        private DelegateCommand _navigateNextPageCommand;

        public DelegateCommand NavigateNextPageCommand => _navigateNextPageCommand ??= new DelegateCommand(NavigateNextPage, CanNavigateNextPage);

        public CharacterCareerSelectionViewModel(IRegionNavigationService regionNavigationService, IAuthenticator authenticator)
            : base(regionNavigationService, authenticator)
        {
            List<CareerModel> careers = new List<CareerModel>()
            {
                new CareerModel()
                {
                    Name = "Agent de la compagnie",
                    Career = Helpers.CareerEnum.CompanyAgent,
                    ImagePath = "https://i.postimg.cc/ryhPgGGs/Agent-De-La-Compagne.png"
                },
                new CareerModel()
                {
                    Name = "Marshal de l'espace",
                    Career = Helpers.CareerEnum.CompanyAgent,
                    ImagePath = "https://i.postimg.cc/ryhPgGGs/Agent-De-La-Compagne.png"
                }
            };

            Careers = new ObservableCollection<CareerModel>(careers);
        }

        public void NavigateNextPage()
        {

        }

        public bool CanNavigateNextPage()
        {
            if (CareerSelection is null) return false;
            if (CareerSelection.SelectedCareer is null) return false;

            return true;
        }

        public bool PersistInHistory()
        {
            return true;
        }
    }
}
