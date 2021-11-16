using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.UI.Models
{
    public class CharacterInfosModel : ModelBase
    {
        public string Name { get; set; }
        public string Appearance { get; set; }
        public string Objectives { get; set; }
        public string Friends { get; set; }
        public string Rivals { get; set; }
        public string FetishItem { get; set; }
        public List<string> LittleItems { get; set; }
    }
}
