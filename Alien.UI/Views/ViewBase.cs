using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Alien.UI.Locators;
using Alien.UI.ViewModels;

namespace Alien.UI.Views
{
    public class ViewBase : ContentControl
    {
        public Border? BusyOverlay { get; private set; }

        public ViewBase()
        {
            ViewModelLocator.SetAutoConnectedViewModelProperty(this, true);

            Loaded += OnLoaded;

            CreateBusyIndicator();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as ViewModelBase;

            viewModel?.OnInit();

            SetBusyIndicatorBinding();
        }

        private void CreateBusyIndicator()
        {
            BusyOverlay = new Border()
            {
                Background = new SolidColorBrush(Color.FromArgb(128, 0, 0, 0)),
                Visibility = Visibility.Collapsed,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Child = new Image()
                {
                    Source = new BitmapImage(new Uri("pack://application:,,,/Alien.UI;component/Assets/Loading.gif")),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                }
            };

            Content = new Grid()
            {
                Children =
                {
                    new ContentPresenter(),
                    BusyOverlay
                }
            };
        }

        // TODO: Make this thing work
        private void SetBusyIndicatorBinding()
        {
            var binding = new Binding("IsBusy")
            {
                Converter = new BooleanToVisibilityConverter()
            };

            BusyOverlay?.SetBinding(VisibilityProperty, binding);
        }
    }
}