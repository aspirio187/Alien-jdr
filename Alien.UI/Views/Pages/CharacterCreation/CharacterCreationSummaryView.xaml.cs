﻿using System;
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
    /// Interaction logic for CharacterSummeryView.xaml
    /// </summary>
    public partial class CharacterCreationSummaryView : ContentControl
    {
        public CharacterCreationSummaryView()
        {
            InitializeComponent();
        }

        private void CheckBox_Changed(object sender, RoutedEventArgs e)
        {
            if ((sender as CheckBox).IsChecked == true)
            {
                btnOver.Content = "Suivant";
            }
            else
            {
                btnOver.Content = "Terminer";
            }
        }
    }
}
