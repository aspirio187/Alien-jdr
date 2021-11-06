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
using System.Windows.Shapes;


namespace Alien.UI.Views
{
    /// <summary>
    /// Logique d'interaction pour ShellView.xaml
    /// </summary>
    public partial class ShellView : Window
    {
        public int OldWidth { get; private set; }
        public int OldHeight { get; private set; }
        public int OldTop { get; private set; }
        public int OldLeft { get; private set; }

        public ShellView()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            OldWidth = (int)Width;
            OldHeight = (int)Height;
            OldTop = (int)Top;
            OldLeft = (int)Left;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            int MaxHeight = (int)SystemParameters.WorkArea.Height;
            int MaxWidth = (int)SystemParameters.WorkArea.Width;
            if (Width != MaxWidth && Height != MaxHeight)
            {
                OldWidth = (int)Width;
                OldHeight = (int)Height;
            }
        }

        private void DockPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
                OldTop = (int)Top;
                OldLeft = (int)Left;
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            int MaxHeight = (int)SystemParameters.WorkArea.Height;
            int MaxWidth = (int)SystemParameters.WorkArea.Width;
            if (Width != MaxWidth && Height != MaxHeight)
            {
                WindowState = WindowState.Normal;
                Top = 0;
                Left = 0;
                Width = MaxWidth;
                Height = MaxHeight;
            }
            else
            {
                WindowState = WindowState.Normal;
                Top = OldTop;
                Left = OldLeft;
                Width = OldWidth;
                Height = OldHeight;
            }
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
    }
}
