using Alien.UI.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.UI.Models
{
    public class CharacterPublicInfosModel : ModelBase
    {
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

        private int _strength;

        public int Strength
        {
            get { return _strength; }
            set
            {
                _strength = value;
                NotifyPropertyChanged();
            }
        }

        private int _agility;

        public int Agility
        {
            get { return _agility; }
            set
            {
                _agility = value;
                NotifyPropertyChanged();
            }
        }

        private int _mind;

        public int Mind
        {
            get { return _mind; }
            set
            {
                _mind = value;
                NotifyPropertyChanged();
            }
        }

        private int _empathy;

        public int Empathy
        {
            get { return _empathy; }
            set
            {
                _empathy = value;
                NotifyPropertyChanged();
            }
        }

        private int _heavyMachines;

        public int HeavyMachines
        {
            get { return _heavyMachines; }
            set
            {
                _heavyMachines = value;
                NotifyPropertyChanged();
            }
        }

        private int _stamina;

        public int Stamina
        {
            get { return _stamina; }
            set
            {
                _stamina = value;
                NotifyPropertyChanged();
            }
        }

        private int _closeCombat;

        public int CloseCombat
        {
            get { return _closeCombat; }
            set
            {
                _closeCombat = value;
                NotifyPropertyChanged();
            }
        }

        private int _mobility;

        public int Mobility
        {
            get { return _mobility; }
            set
            {
                _mobility = value;
                NotifyPropertyChanged();
            }
        }

        private int _piloting;

        public int Piloting
        {
            get { return _piloting; }
            set
            {
                _piloting = value;
                NotifyPropertyChanged();
            }
        }

        private int _rangeCombat;

        public int RangeCombat
        {
            get { return _rangeCombat; }
            set
            {
                _rangeCombat = value;
                NotifyPropertyChanged();
            }
        }

        private int _observation;

        public int Observation
        {
            get { return _observation; }
            set
            {
                _observation = value;
                NotifyPropertyChanged();
            }
        }

        private int _comtech;

        public int Comtech
        {
            get { return _comtech; }
            set
            {
                _comtech = value;
                NotifyPropertyChanged();
            }
        }

        private int _survival;

        public int Survival
        {
            get { return _survival; }
            set
            {
                _survival = value;
                NotifyPropertyChanged();
            }
        }

        private int _manipulation;

        public int Manipulation
        {
            get { return _manipulation; }
            set
            {
                _manipulation = value;
                NotifyPropertyChanged();
            }
        }

        private int _commandment;

        public int Commandment
        {
            get { return _commandment; }
            set
            {
                _commandment = value;
                NotifyPropertyChanged();
            }
        }

        private int _medicalCare;

        public int MedicalCare
        {
            get { return _medicalCare; }
            set
            {
                _medicalCare = value;
                NotifyPropertyChanged();
            }
        }
    }
}
