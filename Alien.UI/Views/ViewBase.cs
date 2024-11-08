using System.Windows;
using System.Windows.Controls;
using Alien.UI.Locators;
using Alien.UI.ViewModels;

namespace Alien.UI.Views
{
    public class ViewBase : ContentControl
    {
        public ViewBase()
        {
            ViewModelLocator.SetAutoConnectedViewModelProperty(this, true);

            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as ViewModelBase;

            viewModel?.OnInit();
        }
    }
}