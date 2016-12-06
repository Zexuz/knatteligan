using System;
using System.Collections.Generic;
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
using MahApps.Metro.Controls;

namespace FootballLeagueManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Create_League_OnClick(object sender, RoutedEventArgs e)
        {
            CreateLeagueHeader.FontWeight = FontWeights.Bold;
            CreateLeagueColumn.IsEnabled = true;
            CreateLeague.IsEnabled = false;
        }

        private void AddTeam_OnClick(object sender, RoutedEventArgs e)
        {
            AddTeamHeader.FontWeight = FontWeights.ExtraBold;
            AddTeamColumn.IsEnabled = true;
            CreateLeagueColumn.IsEnabled = false;
            CreateLeagueHeader.FontWeight = FontWeights.UltraLight;
        }

        private void AddPlayer_OnClick(object sender, RoutedEventArgs e)
        {
            AddPlayerHeader.FontWeight = FontWeights.ExtraBold;
            AddPlayerColumn.IsEnabled = true;
            AddTeamColumn.IsEnabled = false;
            AddTeamHeader.FontWeight = FontWeights.UltraLight;
        }

        private void LeagueList_OnDoubleClick(object sender, MouseButtonEventArgs e)
        {
            CreateLeagueColumn.Visibility = Visibility.Hidden;
            AddPlayerColumn.Visibility = Visibility.Hidden;
            AddTeamColumn.Visibility = Visibility.Hidden;
            CreateLeagueHeader.Visibility = Visibility.Hidden;
            AddTeamHeader.Visibility = Visibility.Hidden;
            AddPlayerHeader.Visibility = Visibility.Hidden;
        }

    }
}
