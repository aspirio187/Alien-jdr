using Alien.BLL.Dtos;
using Alien.BLL.Interfaces;
using Alien.UI.Commands;
using Alien.UI.Managers;
using Alien.UI.States;
using Alien.UI.Views;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Alien.UI.ViewModels
{
    public class CharactersViewModel : ViewModelBase
    {
        private readonly ICharacterService _characterService;
        private readonly NavigationManager _navigationManager;

        private ObservableCollection<CharacterMiniatureDto> _characterMiniatures = new();

        public ObservableCollection<CharacterMiniatureDto> CharacterMiniatures
        {
            get { return _characterMiniatures; }
            set
            {
                _characterMiniatures = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand NavigateCreateCharacterCommand { get; private set; }

        public CharactersViewModel(IAuthenticator authenticator, IMapper mapper, ICharacterService characterService,
            NavigationManager navigationManager)
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

            NavigateCreateCharacterCommand = new RelayCommand(NavigateCreateCharacter);
        }

        public override void OnInit()
        {
            try
            {
                Task
                    .Run(() => _characterService.GetCharactersMiniaturesAsync(_authenticator.User.Id))
                    .ContinueWith(
                        (task) =>
                        {
                            CharacterMiniatures = new ObservableCollection<CharacterMiniatureDto>(task.Result);
                        },
                        TaskScheduler.FromCurrentSynchronizationContext());
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        private void NavigateCreateCharacter()
        {
            _navigationManager.Navigate(nameof(CharacterCareerSelectionView));
        }
    }
}