using Prism.Mvvm;
using Prism.Regions;
using System;
using Alien.UI.Core;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alien.UI.Views;

namespace Alien.UI.ViewModels
{
    public class ShellViewModel : BindableBase
    {
        private readonly RegionManager _regionManager;
        private readonly RegionNavigationService _regionNavigationService;

        public ShellViewModel(RegionManager regionManager, RegionNavigationService regionNavigationService)
        {
            _regionManager = regionManager ?? throw new ArgumentNullException(nameof(regionManager));
            _regionNavigationService = regionNavigationService ?? throw new ArgumentNullException(nameof(regionNavigationService));
        }

        public void Load()
        {
            _regionNavigationService.Region = _regionManager.Regions["MainRegion"];
            _regionNavigationService.RequestNavigate(new Uri("CharacterHomeView", UriKind.Relative), null);
        }
    }

    //public class ShellViewModel : ObservableObject
    //{
    //    public RelayCommand CharacterHomeViewCommand { get; set; }

    //    public RelayCommand GamesViewCommand { get; set; }

    //    public CharacterHomeViewModel CharacterVM { get; set; }

    //    public GameViewModel GamesVM { get; set; }

    //    private object _currentView;

    //    public object CurrentView
    //    {
    //        get { return _currentView; }
    //        set
    //        {
    //            _currentView = value;
    //            OnPropertyChanged();
    //        }
    //    }


    //    public ShellViewModel()
    //    {
    //        CharacterVM = new CharacterHomeView();
    //        GamesVM = new GameViewModel();

    //        CurrentView = CharacterVM;

    //        CharacterHomeViewCommand = new RelayCommand(o =>
    //        {
    //            CurrentView = CharacterVM;
    //        });

    //        GamesViewCommand = new RelayCommand(o =>
    //        {
    //            CurrentView = GamesVM;
    //        });
    //    }
    //}
}
