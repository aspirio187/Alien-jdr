using Alien.UI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.UI.Models
{
    public class CharacterAndroidCreationModel : ModelBase
    {
        private Attributes _attribute;

        public Attributes Attribute
        {
            get { return _attribute; }
            set
            {
                _attribute = value;
                NotifyPropertyChanged();
            }
        }

        private bool _isSelected;

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                NotifyPropertyChanged();
            }
        }
    }
}
