using Alien.BLL.Dtos;
using Alien.UI.Helpers;
using Alien.UI.Models;
using Alien.UI.States;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Alien.UI.Commands;
using Alien.UI.Managers;
using Alien.UI.Views;

namespace Alien.UI.ViewModels
{
    public class CharacterInfosCreationViewModel : ViewModelBase
    {
        private readonly NavigationManager _navigationManager;
        public CharacterCreationDto? CharacterCreation { get; private set; }

        private CharacterInfosCreationModel _characterInfos = new();

        public CharacterInfosCreationModel CharacterInfos
        {
            get { return _characterInfos; }
            set
            {
                _characterInfos = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand NavigateBackCommand { get; private set; }

        public ICommand NavigateNextPageCommand { get; private set; }

        public ICommand AddItemCommand { get; private set; }
        public ICommand RemoveItemCommand { get; private set; }
        public ICommand AddEquipmentCommand { get; private set; }
        public ICommand RemoveEquipmentCommand { get; private set; }

        public CharacterInfosCreationViewModel(IAuthenticator authenticator, IMapper mapper,
            NavigationManager navigationManager)
            : base(authenticator, mapper)
        {
            if (navigationManager is null)
            {
                throw new ArgumentNullException(nameof(navigationManager));
            }

            _navigationManager = navigationManager;

            NavigateBackCommand = new RelayCommand(NavigateBack);
            NavigateNextPageCommand = new RelayCommand(NavigateNextPage);
            AddItemCommand = new RelayCommand(AddItem);
            RemoveItemCommand = new RelayCommand(RemoveItem);
            AddEquipmentCommand = new RelayCommand(AddEquipment);
            RemoveEquipmentCommand = new RelayCommand(RemoveEquipment);
        }

        public void AddItem()
        {
            if (CharacterInfos.NewItemIsValid)
            {
                CharacterInfos.LittleItems.Add(CharacterInfos.NewItem);
                CharacterInfos.NewItem = string.Empty;
            }
        }

        public void RemoveItem()
        {
            if (CharacterInfos.SelectedItem is not null)
            {
                CharacterInfos.LittleItems.Remove(CharacterInfos.SelectedItem);
                CharacterInfos.SelectedItem = null;
            }
        }

        public void AddEquipment()
        {
            if (CharacterInfos.NewEquipmentIsValid)
            {
                CharacterInfos.Equipments.Add(CharacterInfos.NewEquipment);
                CharacterInfos.NewEquipment = string.Empty;
            }
        }

        public void RemoveEquipment()
        {
            if (CharacterInfos.SelectedItem is not null)
            {
                CharacterInfos.Equipments.Remove(CharacterInfos.SelectedEquipment);
                CharacterInfos.SelectedEquipment = null;
            }
        }

        // public override void OnNavigatedTo(NavigationContext navigationContext)
        // {
        //     base.OnNavigatedTo(navigationContext);
        //
        //     CharacterCreation = navigationContext.Parameters.GetValue<CharacterCreationDto>(Global.CHARACTER_CREATION);
        // }

        public void NavigateBack()
        {
            if (_navigationManager.CanNavigateBack())
            {
                _navigationManager.NavigateBack();
            }
        }

        public void NavigateNextPage()
        {
            if (CharacterInfos.IsValid)
            {
                CharacterCreation.Name = CharacterInfos.Name;
                CharacterCreation.Appearance = CharacterInfos.Appearance;
                CharacterCreation.Objectives = CharacterInfos.Objectives;
                CharacterCreation.Friends = CharacterInfos.Friends;
                CharacterCreation.Rivals = CharacterInfos.Rivals;
                CharacterCreation.Items = new List<ItemCreationDto>();

                CharacterCreation.Items.Add(new ItemCreationDto()
                {
                    Name = CharacterInfos.FetishItem,
                    IsFetish = true
                });

                foreach (string item in CharacterInfos.LittleItems)
                {
                    CharacterCreation.Items.Add(new ItemCreationDto()
                    {
                        Name = item,
                        IsFetish = false
                    });
                }

                CharacterCreation.Equipments = new List<string>();
                foreach (string equipment in CharacterInfos.Equipments)
                {
                    CharacterCreation.Equipments.Add(equipment);
                }

                Dictionary<string, object> parameters = new Dictionary<string, object>()
                {
                    { Global.CHARACTER_CREATION, CharacterCreation }
                };

                _navigationManager.Navigate(nameof(CharacterTalentSelectionView), parameters: parameters);
            }
        }
    }
}