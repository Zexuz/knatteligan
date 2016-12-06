using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using knatteligan.Domain.Entities;
using knatteligan.Helpers;

namespace UserHomePage
{
    /// <summary>
    /// Interaction logic for PlayerStatsPage.xaml
    /// </summary>
    public partial class PlayerStatsPage : Page
    {

        public PlayerStatsPage(Team team) : this(new List<Team> {team})
        {
            PlayersDataGrid.Columns[1].Visibility = Visibility.Collapsed;
        }

        public PlayerStatsPage(IEnumerable<Team> teams)
        {
            DataContext = this;
            InitializeComponent();
            PlayersDataGrid.AutoGenerateColumns = false;
            PlayersDataGrid.ItemsSource = SortingAlgorithm.Sort(teams, SortingAlgorithm.PlayerSortByTypes.Goal, true);
        }

    }
}
