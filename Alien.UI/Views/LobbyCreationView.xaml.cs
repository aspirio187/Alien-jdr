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
    /// Interaction logic for GameCreationView.xaml
    /// </summary>
    public partial class LobbyCreationView : ContentControl
    {
        public LobbyCreationView()
        {
            InitializeComponent();
            cmbNumberPlayer.ItemsSource = new List<string>
            {
                "2", "3", "4", "5", "6"
            };
        }
    }
}
