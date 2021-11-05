using Alien.UI.Helpers;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.UI.ViewModels
{
    public class ViewModelBase : BindableBase, INavigationAware
    {
        protected IRegionNavigationService _regionNavigationService;
        public DelegateCommand LoadCommand { get; set; }

        public ViewModelBase(IRegionNavigationService regionNavigationService)
        {
            _regionNavigationService = regionNavigationService ??
                throw new ArgumentNullException(nameof(regionNavigationService));
        }

        /// <summary>
        /// Synchronious loading method
        /// </summary>
        protected virtual void Load()
        {

        }

        /// <summary>
        /// Asynchronious loading method
        /// </summary>
        protected virtual async Task LoadAsync()
        {

        }

        /// <summary>
        /// Navigate to a view registered in the ViewsEnum with custom parameters set to null by default
        /// </summary>
        /// <param name="view">Enumeration containing all the views</param>
        /// <param name="navigationParams">Dictionnary containing all the parameters of all type</param>
        protected virtual void Navigate(ViewsEnum view, Dictionary<string, object> navigationParams = null)
        {
            NavigationParameters navigationParameters = new()
            {
                { Global.NAVIGATION_SERVICE, _regionNavigationService }
            };

            if(navigationParams is not null)
            {
                foreach (var navigationParam in navigationParams)
                {
                    navigationParameters.Add(navigationParam.Key, navigationParam.Value);
                }
            }
            _regionNavigationService.RequestNavigate(new Uri(view.ToString(), UriKind.Relative), navigationParameters);
        }

        public virtual bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public virtual void OnNavigatedFrom(NavigationContext navigationContext)
        {
            // rien pour l'instant
        }

        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (_regionNavigationService is null) _regionNavigationService = navigationContext.Parameters.GetValue<IRegionNavigationService>(Global.NAVIGATION_SERVICE);
        }
    }
}
