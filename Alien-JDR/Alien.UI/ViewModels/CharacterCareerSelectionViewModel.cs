using Alien.UI.Models;
using Alien.UI.States;
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

        public CareerModel SelectedCareer { get; set; }

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

        public bool PersistInHistory()
        {
            return true;
        }
    }
}
