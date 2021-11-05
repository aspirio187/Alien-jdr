using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.UI.Helpers
{
    public static class Global
    {
        public const string REGION_NAME = "MainRegion";
        public const string NAVIGATION_SERVICE  = "NavigationService";
    }

    public enum ViewsEnum
    {
        CharactersView,
        PartiesView,
        HistoryView,
        NotificationView
    }
}
