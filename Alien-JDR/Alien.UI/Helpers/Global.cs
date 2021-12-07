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
        public const string CHARACTER_CREATION = "Creating Character";


        /* Global */

        public const string CHANEL_PING = "CPing";

        /* Joueur */

        /* Mj */

        public const string MJ_CHANEL_PLAY = "Play";
        public const string MJ_CHANEL_NEW_PLAYER = "NewPlayer";
        public const string MJ_CHANEL_ADD_ITEM = "AddItem";
        public const string MJ_CHANEL_REMOVE_ITEM = "RemoveItem";
        public const string MJ_CHANEL_ADD_EQUIPEMENT = "AddEquipement";
        public const string MJ_CHANEL_REMOVE_EQUIPEMENT = "RemoveEquipement";

        public const string CHANEL_ADD_ITEM = "AddItem";

        public static readonly Dictionary<Competences?, string> Competences = new()
        {
            { Helpers.Competences.HeavyMachines, "Machines lourdes" },
            { Helpers.Competences.Stamina, "Endurance" },
            { Helpers.Competences.CloseCombat, "Combat rapproché" },
            { Helpers.Competences.Mobility, "Mobilité" },
            { Helpers.Competences.Piloting, "Pilotage" },
            { Helpers.Competences.RangeCombat, "Combat à distance" },
            { Helpers.Competences.Observation, "Observation" },
            { Helpers.Competences.Comtech, "Comtech" },
            { Helpers.Competences.Survival, "Survie" },
            { Helpers.Competences.Manipulation, "Manipulation" },
            { Helpers.Competences.Commandment, "Commandement" },
            { Helpers.Competences.MedicalCare, "Soins médicaux" }
        };
    }

    public enum ViewsEnum
    {
        CharactersView,
        PartiesView,
        HistoryView,
        NotificationView,
        ManuelView,
        CreditView,
        CharacterCareerSelectionView,
        CharacterInfosCreationView,
        CharacterTalentSelectionView,
        CharacterAttributesCompetencesView,
        CharacterAndroidCreationView,
        CharacterCreationSummaryView,
        CharacterPublicInfosView
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
        Denied,
        Pending
    }

    public enum CareerEnum
    {
        CompanyAgent
    }

    public enum Attributes
    {
        Force,
        Agilité,
        Esprit,
        Empathie
    }

    public enum Competences
    {
        HeavyMachines,
        Stamina,
        CloseCombat,
        Mobility,
        Piloting,
        RangeCombat,
        Observation,
        Comtech,
        Survival,
        Manipulation,
        Commandment,
        MedicalCare
    }
}
