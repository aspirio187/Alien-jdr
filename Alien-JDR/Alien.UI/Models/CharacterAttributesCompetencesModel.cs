using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Alien.UI.Models
{
    public class CharacterAttributesCompetencesModel : ModelBase
    {
        private int _strength = 2;

        public int Strength
        {
            get { return _strength; }
            set
            {
                _strength = value;
                NotifyPropertyChanged();
            }
        }

        private int _agility = 2;

        public int Agility
        {
            get { return _agility; }
            set
            {
                _agility = value;
                NotifyPropertyChanged();
            }
        }

        private int _mind = 2;

        public int Mind
        {
            get { return _mind; }
            set
            {
                _mind = value;
                NotifyPropertyChanged();
            }
        }

        private int _empathy = 2;

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
