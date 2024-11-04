using Alien.BLL.Dtos;
using Alien.BLL.Interfaces;
using Alien.UI.Commands;
using Alien.UI.Helpers;
using Alien.UI.Managers;
using Alien.UI.States;
using Alien.UI.Views;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Alien.UI.ViewModels
{
    public class CharacterCreationSummaryViewModel : ViewModelBase
    {
        private readonly ICharacterService _characterService;
        private readonly NavigationManager _navigationManager;

        private CharacterCreationDto _characterCreation = new();

        public CharacterCreationDto CharacterCreation
        {
            get { return _characterCreation; }
            set
            {
                _characterCreation = value;
                NotifyPropertyChanged();
            }
        }

        private string _careerDescription;

        public string CareerDescription
        {
            get { return _careerDescription; }
            set
            {
                _careerDescription = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand NavigateBackCommand { get; private set; }
        public ICommand CreateCharacterCommand { get; private set; }

        public CharacterCreationSummaryViewModel(IAuthenticator authenticator, IMapper mapper, ICharacterService characterService, NavigationManager navigationManager)
            : base(authenticator, mapper)
        {
            if (characterService is null)
            {
                throw new ArgumentNullException(nameof(characterService));
            }

            if (navigationManager is null)
            {
                throw new ArgumentNullException(nameof(navigationManager));
            }

            _characterService = characterService;
            _navigationManager = navigationManager;

            NavigateBackCommand = new RelayCommand(NavigateBack);
            CreateCharacterCommand = new RelayCommand(CreateCharacter);
        }

        public void NavigateBack()
        {
            if (_navigationManager.CanNavigateBack())
            {
                _navigationManager.NavigateBack();
            }
        }

        public async void CreateCharacter()
        {
            try
            {
                if (await _characterService.CreateCharacter(CharacterCreation, _authenticator.User.Id))
                {
                    if (CharacterCreation.IsPublic)
                    {
                        //_regionNavigationService.Journal.Clear();
                        _navigationManager.Navigate(nameof(CharactersView));
                    }
                    else
                    {
                        Dictionary<string, object> parameters = new Dictionary<string, object>()
                        {
                            { Global.CHARACTER_CREATION, CharacterCreation }
                        };

                        _navigationManager.Navigate(nameof(CharacterPublicInfosView), parameters: parameters);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        //public override void OnNavigatedTo(NavigationContext navigationContext)
        //{
        //    base.OnNavigatedTo(navigationContext);

        //    CharacterCreation = new();
        //    CharacterCreation = navigationContext.Parameters.GetValue<CharacterCreationDto>(Global.CHARACTER_CREATION);
        //    if (CharacterCreation is not null && !string.IsNullOrEmpty(CharacterCreation.Career))
        //    {
        //        CareerDescription = _characterService.GetCareer(CharacterCreation.Career).Description;
        //    }
        //}

        public bool PersistInHistory()
        {
            return true;
        }
    }
}
