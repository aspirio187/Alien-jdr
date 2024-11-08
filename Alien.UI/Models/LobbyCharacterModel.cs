﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.UI.Models
{
    public class LobbyCharacterModel : ModelBase
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set
            {
                ValidateProperty(ref _id, value);
                NotifyPropertyChanged();
            }
        }

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

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
