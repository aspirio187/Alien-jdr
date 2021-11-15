using Alien.UI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.UI.Models
{
    public class CareerModel
    {
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public CareerEnum Career { get; set; }
    }
}
