using Alien.UI.Helpers;
using Alien.UI.States;
using AutoMapper;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.UI.ViewModels
{
    public class ViewModelBase : BindableBase, INavigationAware
    {
        protected readonly IAuthenticator _authenticator;
        protected readonly IMapper _mapper;

        protected IRegionNavigationService _regionNavigationService;

        public IRegionNavigationService RegionNavigationService => _regionNavigationService;

        protected DelegateCommand _loadCommand;
        public virtual DelegateCommand LoadCommand => _loadCommand;

        public ViewModelBase(IRegionNavigationService regionNavigationService, IAuthenticator authenticator, IMapper mapper)
        {
            _regionNavigationService = regionNavigationService ??
                throw new ArgumentNullException(nameof(regionNavigationService));
            _authenticator = authenticator ??
                throw new ArgumentNullException(nameof(authenticator));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
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
            try
            {
                NavigationParameters navigationParameters = new()
                {
                    { Global.NAVIGATION_SERVICE, _regionNavigationService }
                };

                if (navigationParams is not null)
                {
                    foreach (var navigationParam in navigationParams)
                    {
                        navigationParameters.Add(navigationParam.Key, navigationParam.Value);
                    }
                }
                _regionNavigationService.RequestNavigate(new Uri(view.ToString(), UriKind.Relative), navigationParameters);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
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
            if (_regionNavigationService is null || _regionNavigationService.Region is null)
                _regionNavigationService = navigationContext.Parameters.GetValue<IRegionNavigationService>(Global.NAVIGATION_SERVICE);
        }
    }
}
