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
    /// Logique d'interaction pour GamesView.xaml
    /// </summary>
    public partial class GamesView : UserControl
    {
        public GamesView()
        {
            InitializeComponent();
            games game1 = new games();

            game1.gameID = "1";
            game1.playerID = "David";
            game1.gameMode = "Compagne";
            game1.creator = "Soultan";

            GamesDataGrid.Items.Add(game1);

            games game2 = new games();

            game2.gameID = "2";
            game2.playerID = "Soultan";
            game2.gameMode = "Custom";
            game2.creator = "David";

            GamesDataGrid.Items.Add(game2);
        }

        public class games
        {
            public string gameID { get; set; }
            public string playerID { get; set; }
            public string gameMode { get; set; }
            public string creator { get; set; }
        }

        private void CreateAGame_Click(object sender, RoutedEventArgs e)
        {
            games tempGames = new games();

            tempGames.gameID = gameID.Text;
            tempGames.playerID = playerID.Text;
            tempGames.gameMode = gameMode.Text;
            tempGames.creator = creator.Text;

            GamesDataGrid.Items.Add(tempGames);
        }
    }
}
