using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.UI.Helpers
{
    public static class EnumConversion
    {
        public static string ConvertToString(this RaceEnum race) => race switch
        {
            RaceEnum.Humain => "Human",
            RaceEnum.Android => "Android",
            _ => null,
        };
    }
}
