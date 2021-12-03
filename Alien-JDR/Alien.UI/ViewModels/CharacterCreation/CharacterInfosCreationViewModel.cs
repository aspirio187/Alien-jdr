using Alien.BLL.Dtos;
using Alien.UI.Helpers;
using Alien.UI.Models;
using Alien.UI.States;
using Prism.Commands;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.UI.ViewModels
{
    public class CharacterInfosCreationViewModel : ViewModelBase, IJournalAware
    {
        public CharacterCreationDto CharacterCreation { get; private set; }

        private CharacterInfosCreationModel _characterInfos = new();

        public CharacterInfosCreationModel CharacterInfos
        {
            get { return _characterInfos; }
            set { SetProperty(ref _characterInfos, value); }
        }

        private DelegateCommand _navigateBackCommand;
        private DelegateCommand _navigateNextPageCommand;
        private DelegateCommand _addItemCommand;
        private DelegateCommand _removeItemCommand;
        private DelegateCommand _addEquipmentCommand;
        private DelegateCommand _removeEquipmentCommand;

        public DelegateCommand NavigateBackCommand => _navigateBackCommand ??= new DelegateCommand(NavigateBack);
        public DelegateCommand NavigateNextPageCommand => _navigateNextPageCommand ??= new DelegateCommand(NavigateNextPage);
        public DelegateCommand AddItemCommand => _addItemCommand ??= new(AddItem);
        public DelegateCommand RemoveItemCommand => _removeItemCommand ??= new(RemoveItem);
        public DelegateCommand AddEquipmentCommand => _addEquipmentCommand ??= new(AddEquipment);
        public DelegateCommand RemoveEquipmentCommand => _removeEquipmentCommand ??= new(RemoveEquipment);

        public CharacterInfosCreationViewModel(IRegionNavigationService regionNavigationService, IAuthenticator authenticator)
            : base(regionNavigationService, authenticator)
        {

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
            if (!string.IsNullOrEmpty(CharacterInfos.NewEquipment))
            {
                if (!CharacterInfos.Equipments.Any(e => e.Equals(CharacterInfos.NewEquipment)))
                {
                    CharacterInfos.Equipments.Add(CharacterInfos.NewEquipment);
                    CharacterInfos.NewEquipment = string.Empty;
                }
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

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            CharacterCreation = navigationContext.Parameters.GetValue<CharacterCreationDto>(Global.CHARACTER_CREATION);
        }

        public void NavigateBack()
        {
            if (_regionNavigationService.Journal.CanGoBack)
            {
                _regionNavigationService.Journal.GoBack();
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

                Navigate(ViewsEnum.CharacterTalentSelectionView, parameters);
            }
        }

        public bool PersistInHistory()
        {
            return true;
        }
    }
}