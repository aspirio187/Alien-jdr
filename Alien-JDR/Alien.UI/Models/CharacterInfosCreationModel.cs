using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.UI.Models
{
    public class CharacterInfosCreationModel : ModelBase
    {
        private string _name;

        [Required(AllowEmptyStrings = false, ErrorMessage = "Le nom du personnage est requis!")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Le nom du personnage doit faire entre 5 et 50 caractères!")]
        public string Name
        {
            get => _name;
            set
            {
                ValidateProperty(ref _name, value);
                NotifyPropertyChanged();
            }
        }

        private string _appearance;

        [Required(AllowEmptyStrings = false, ErrorMessage = "Une description de l'apparence du personnage est requise!")]
        [StringLength(150, MinimumLength = 25, ErrorMessage = "L'apparence doit faire entre 25 et 150 caractères!")]
        public string Appearance
        {
            get { return _appearance; }
            set
            {
                ValidateProperty(ref _appearance, value);
                NotifyPropertyChanged();
            }
        }

        private string _objectives;

        [Required(AllowEmptyStrings = false, ErrorMessage = "Les objectifs du personnage sont requis!")]
        [StringLength(150, MinimumLength = 25, ErrorMessage = "Les objectifs du personnage doivent faire entre 25 et 150 caractères!")]
        public string Objectives
        {
            get { return _objectives; }
            set
            {
                ValidateProperty(ref _objectives, value);
                NotifyPropertyChanged();
            }
        }

        private string _friends;

        [Required(AllowEmptyStrings = false, ErrorMessage = "Le personnage doit avoir au moins un ami")]
        [MinLength(5, ErrorMessage = "Le nom de l'ami doit faire au moins 5 caractères!")]
        [MaxLength(150, ErrorMessage = "Les noms de tous les amis réunis ne peuvent pas dépasser 150 caractères!")]
        public string Friends
        {
            get { return _friends; }
            set
            {
                ValidateProperty(ref _friends, value);
                NotifyPropertyChanged();
            }
        }

        private string _rivals;

        [Required(AllowEmptyStrings = false, ErrorMessage = "Le personnage doit avoir au moins un rival!")]
        [MinLength(5, ErrorMessage = "Le nom du rival doit faire au moins 5 caractères!")]
        [MaxLength(150, ErrorMessage = "Les noms des amis réunis ne peuvent pas dépasser les 150 caractères!")]
        public string Rivals
        {
            get { return _rivals; }
            set
            {
                ValidateProperty(ref _rivals, value);
                NotifyPropertyChanged();
            }
        }

        private string _fetishItem;

        [Required(AllowEmptyStrings = false, ErrorMessage = "L'objet fétiche est requis!")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "L'élément fétiche doit faire entre 2 et 50 caractères!")]
        public string FetishItem
        {
            get { return _fetishItem; }
            set
            {
                ValidateProperty(ref _fetishItem, value);
                NotifyPropertyChanged();
            }
        }

        private string _newItem;

        [StringLength(50, ErrorMessage = "Un élément doit faire entre 2 et 50 caractères!")]
        public string NewItem
        {
            get { return _newItem; }
            set
            {
                ValidateProperty(ref _newItem, value);
                NotifyPropertyChanged();
            }
        }

        public bool NewItemIsValid
        {
            get
            {
                return NewItem is not null &&
                    Validator.TryValidateProperty(NewItem, new ValidationContext(this) { MemberName = nameof(NewItem) }, CleanResults(ValidationResults)) ||
                    NewItem?.Length > 2 ||
                    !LittleItems.Contains(NewItem);
            }
        }

        public string SelectedItem { get; set; }
        public ObservableCollection<string> LittleItems { get; set; } = new();

        private string _newEquipment;

        [StringLength(50, ErrorMessage = "L'équipement doit faire entre 2 et 50 caractères!")]
        public string NewEquipment
        {
            get { return _newEquipment; }
            set
            {
                ValidateProperty(ref _newEquipment, value);
                NotifyPropertyChanged();
            }
        }

        public bool NewEquipmentIsValid
        {
            get
            {
                return NewEquipment is not null &&
                    Validator.TryValidateProperty(NewEquipment, new ValidationContext(this) { MemberName = nameof(NewEquipment) }, CleanResults(ValidationResults)) ||
                    NewEquipment?.Length > 2 ||
                    !Equipments.Contains(NewEquipment);
            }
        }

        public string SelectedEquipment { get; set; }
        public ObservableCollection<string> Equipments { get; set; } = new();
    }
}
