﻿using Alien.UI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.UI.Models
{
    public class CharacterCareerSelectionModel : ModelBase
    {
        public CareerModel SelectedCareer { get; set; }
        public RaceEnum Race { get; set; }
    }
}
