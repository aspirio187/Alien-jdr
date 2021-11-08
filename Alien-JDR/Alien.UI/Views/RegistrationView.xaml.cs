using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Alien.UI.Views
{
    /// <summary>
    /// Logique d'interaction pour RegistrationView.xaml
    /// </summary>
    public partial class RegistrationView : UserControl
    {
        public const string USERNAME = "USERNAME";
        public RegistrationView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            txbUsername.Text = USERNAME;
        }

        private void DockPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Window parent = Window.GetWindow(this);
            if (e.ChangedButton == MouseButton.Left)
            {
                parent?.DragMove();
            }
        }

        private void txbUsername_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txbUsername.Text.Equals(USERNAME))
            {
                txbUsername.Text = string.Empty;
            }
        }
    }
}
