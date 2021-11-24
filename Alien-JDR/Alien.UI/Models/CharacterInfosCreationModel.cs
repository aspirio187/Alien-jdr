using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.UI.Models
{
    public class CharacterInfosCreationModel : ModelBase
    {
        public string Name { get; set; }
        public string Appearance { get; set; }
        public string Objectives { get; set; }
        public string Friends { get; set; }
        public string Rivals { get; set; }
        public string FetishItem { get; set; }
        public string NewItem { get; set; }
        public string SelectedItem { get; set; }
        public ObservableCollection<string> LittleItems { get; set; } = new();
        public string NewEquipment { get; set; }
        public string SelectedEquipment { get; set; }
        public ObservableCollection<string> Equipments { get; set; } = new();
    }
}
