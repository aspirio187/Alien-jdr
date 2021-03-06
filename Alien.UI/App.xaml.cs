using Prism.Unity;
using Prism.Ioc;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Alien.UI.Views;
using Alien.UI.States;
using Alien.BLL;

namespace Alien.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<ShellView>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.InjectNTier();

            containerRegistry.RegisterSingleton<IAuthenticator, Authenticator>();

            containerRegistry.RegisterDialog<LoginView>();
            containerRegistry.RegisterDialog<RegistrationView>();

            containerRegistry.RegisterForNavigation<ManuelView>();
            containerRegistry.RegisterForNavigation<CreditView>();

            // Character views
            containerRegistry.RegisterForNavigation<CharactersView>();
            containerRegistry.RegisterForNavigation<CharacterCareerSelectionView>();
            containerRegistry.RegisterForNavigation<CharacterInfosCreationView>();
            containerRegistry.RegisterForNavigation<CharacterTalentSelectionView>();
            containerRegistry.RegisterForNavigation<CharacterAttributesCompetencesView>();
            containerRegistry.RegisterForNavigation<CharacterAndroidCreationView>();
            containerRegistry.RegisterForNavigation<CharacterCreationSummaryView>();
            containerRegistry.RegisterForNavigation<CharacterPublicInfosView>();

            // Lobby View
            containerRegistry.RegisterForNavigation<LobbiesView>();
            containerRegistry.RegisterForNavigation<LobbyCreationView>();

            // Notification View
            containerRegistry.RegisterForNavigation<NotificationsView>();

        }
    }
}
