using Alien.UI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.UI.Models
{
    public class CharacterCareerSelectionModel : ModelBase
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                ValidateProperty(ref _name, value);
                NotifyPropertyChanged();
            }
        }

        private string _imagePath;

        public string ImagePath
        {
            get { return _imagePath; }
            set
            {
                ValidateProperty(ref _imagePath, value);
                NotifyPropertyChanged();
            }
        }

        private string _description;

        public string Description
        {
            get { return _description; }
            set
            {
                ValidateProperty(ref _description, value);
                NotifyPropertyChanged();
            }
        }
    }
}