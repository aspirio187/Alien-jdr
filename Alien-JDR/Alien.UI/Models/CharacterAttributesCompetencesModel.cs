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

        public int Agility { get; set; }
        public int Mind { get; set; }
        public int Empathy { get; set; }
    }
}
