using Alien.BLL.Dtos;
using Alien.UI.Commands;
using Alien.UI.Helpers;
using Alien.UI.Managers;
using Alien.UI.Models;
using Alien.UI.States;
using Alien.UI.Views;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Alien.UI.ViewModels
{
    public class CharacterAndroidCreationViewModel : ViewModelBase
    {
        private readonly NavigationManager _navigationManager;

        public CharacterCreationDto CharacterCreation { get; set; }

        public ObservableCollection<bool> SelectedAttributes { get; set; } = new()
        {
            false,
            false,
            false,
            false
        };

        public ICommand NavigateBackCommand { get; private set; }
        public ICommand NavigateNextPageCommand { get; private set; }
        public ICommand SelectAttributeCommand { get; private set; }

        public CharacterAndroidCreationViewModel(IAuthenticator authenticator, IMapper mapper, NavigationManager navigationManager)
            : base(authenticator, mapper)
        {
            if (navigationManager is null)
            {
                throw new ArgumentNullException(nameof(navigationManager));
            }

            _navigationManager = navigationManager;

            NavigateBackCommand = new RelayCommand(NavigateBack);
            NavigateNextPageCommand = new RelayCommand(NavigateNextPage, CanNavigateNextPage);
            SelectAttributeCommand = new RelayCommand<Attributes?>(SelectAttribute, CanSelectAttribute);

            SelectAttributeCommand.CanExecute(null);
            _navigationManager = navigationManager;
        }

        public void NavigateBack()
        {
            if (_navigationManager.CanNavigateBack())
            {
                _navigationManager.NavigateBack();
            }
        }

        public bool CanSelectAttribute(Attributes? attribute)
        {
            return attribute is not null && (SelectedAttributes.Count(a => a) < 2 || SelectedAttributes[(int)attribute]);
        }

        public void SelectAttribute(Attributes? attribute)
        {
            if (attribute is null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }

            SelectedAttributes[(int)attribute] = !SelectedAttributes[(int)attribute];
            NavigateNextPageCommand.CanExecute(null);
            SelectAttributeCommand.CanExecute(null);
        }

        public bool CanNavigateNextPage()
        {
            return SelectedAttributes.Count(a => a) == 2;
        }

        public void NavigateNextPage()
        {
            if (SelectedAttributes[0]) CharacterCreation.Strength += 3;
            if (SelectedAttributes[1]) CharacterCreation.Agility += 3;
            if (SelectedAttributes[2]) CharacterCreation.Mind += 3;
            if (SelectedAttributes[3]) CharacterCreation.Empathy += 3;

            CharacterCreation.SelectedAttributes = SelectedAttributes.ToArray();

            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { Global.CHARACTER_CREATION, CharacterCreation }
            };

            _navigationManager.Navigate(nameof(CharacterCreationSummaryView), parameters: parameters);
        }

        //public override void OnNavigatedTo(NavigationContext navigationContext)
        //{
        //    base.OnNavigatedTo(navigationContext);

        //    CharacterCreation = navigationContext.Parameters.GetValue<CharacterCreationDto>(Global.CHARACTER_CREATION);
        //}
    }
}
