using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Alien.UI.States;
using Alien.BLL;
using System.Threading;
using System.Globalization;
using Alien.UI.Managers;
using Microsoft.Extensions.DependencyInjection;
using Alien.UI.ViewModels;
using Alien.UI.Views;

namespace Alien.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; } = default!;

        public App()
        {
            // Set the current thread to the french culture
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("fr-FR");
            Thread.CurrentThread.CurrentCulture = new CultureInfo("fr-FR");

            // Initialize ServiceCollection
            ServiceCollection serviceCollection = new ServiceCollection();

            ConfigureServices(serviceCollection);

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            ShellView shellView = new();

            shellView.Show();
        }

        private void ConfigureServices(ServiceCollection services)
        {
            // Implemenrt a correct NTier injection
            services.InjectNTier();

            services.AddSingleton<IAuthenticator, Authenticator>();

            services.AddSingleton<NavigationManager>();

            services.AddTransient<LoginViewModel>();
            services.AddTransient<RegistrationViewModel>();

            //services.AddTransient<ManuelView>();
            //services.AddTransient<CreditView>();

            // Character views
            services.AddTransient<ShellViewModel>();
            services.AddTransient<CharactersViewModel>();
            services.AddTransient<CharacterCareerSelectionViewModel>();
            services.AddTransient<CharacterInfosCreationViewModel>();
            services.AddTransient<CharacterTalentSelectionViewModel>();
            services.AddTransient<CharacterAttributesCompetencesViewModel>();
            services.AddTransient<CharacterAndroidCreationViewModel>();
            services.AddTransient<CharacterCreationSummaryViewModel>();
            services.AddTransient<CharacterPublicInfosViewModel>();

            // Lobby View
            services.AddTransient<LobbiesViewModel>();
            services.AddTransient<LobbyCreationViewModel>();


            // Notification View
            services.AddTransient<NotificationsViewModel>();
        }
    }
}