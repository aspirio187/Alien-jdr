using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Alien.UI.Helpers
{
    public class EnumBoolValuesConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0].GetType() != typeof(LobbyModeEnum))
                throw new ArgumentException($"The first value : \"{ values[0] }\" is type : \"{ values[0].GetType() }\" and must be of type : \"{ typeof(LobbyModeEnum) }\"");

            LobbyModeEnum lobbyMode = (LobbyModeEnum)values[0];

            if (values[1].GetType() != typeof(bool))
                throw new ArgumentException($"The second value : \"{ values[1] }\" is type : \"{ values[1].GetType() }\" and must be of type : \"{ typeof(bool) }\"");

            bool isCreator = (bool)values[1];

            if (values[2].GetType() != typeof(int) || values[3].GetType() != typeof(int))
            {
                throw new ArgumentException(
                    $"The third and fourth values\n" +
                    $"\tValue 3 : \"{ values[2] }\" is type : \"{ values[2].GetType() }\"\n" +
                    $"\tValue 4 : \"{ values[3] }\" is type : \"{ values[3].GetType() }\"\n" +
                    $"must be type : \"{ typeof(int) }\"");
            }

            int rowId = (int)values[2];
            int userId = (int)values[3];

            if (isCreator)
            {
                return true;
            }
            else if (lobbyMode.Equals(LobbyModeEnum.Scenario) && isCreator == false)
            {
                return false;
            }
            else if (rowId != userId)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
