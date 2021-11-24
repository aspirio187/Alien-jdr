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
    /// Logique d'interaction pour CharacterInfosView.xaml
    /// </summary>
    public partial class CharacterInfosCreationView : ContentControl
    {
        public CharacterInfosCreationView()
        {
            InitializeComponent();
        }

        private void Add_GotFocus(object sender, RoutedEventArgs e)
        {
            var textbox = sender as TextBox;
            textbox.Text = string.Empty;
        }
    }
}