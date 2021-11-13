using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.UI.Helpers
{
    public static class Global
    {
        public const string REGION_NAME = "MainRegion";
        public const string NAVIGATION_SERVICE = "NavigationService";
        public const string SESSION_PATH = "Session";
    }

    public enum ViewsEnum
    {
        CharactersView,
        PartiesView,
        HistoryView,
        NotificationView
    }

    public enum RaceEnum
    {
        Humain,
        Android
    }

    public enum PartyModeEnum
    {
        Scenario,
        Campagned
    }

    public enum NotificationStatusEnum
    {
        Accepted,
        Denied
    }
}
