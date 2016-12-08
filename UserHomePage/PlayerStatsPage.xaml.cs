using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using knatteligan.Domain.Entities;
using knatteligan.Helpers;
using knatteligan.Services;

namespace UserHomePage
{
    /// <summary>
    /// Interaction logic for PlayerStatsPage.xaml
    /// </summary>
    public partial class PlayerStatsPage : Page
    {
        private readonly LeagueService _leagueService;

        public PlayerStatsPage(Team team) : this(new List<Team> {team})
        {
            PlayersDataGrid.Columns[1].Visibility = Visibility.Collapsed;
            Header.Text = team.Name.Value;
            SubHeader.Text = _leagueService.GetAll().First(x => x.TeamIds.Contains(team.Id)).Name.Value;
            SubHeader.Visibility = Visibility.Visible;

        }

        public PlayerStatsPage(IEnumerable<Team> teams)
        {
            
            DataContext = this;
            InitializeComponent();
            SubHeader.Visibility = Visibility.Collapsed;
            PlayersDataGrid.AutoGenerateColumns = false;
            PlayersDataGrid.ItemsSource = SortingAlgorithm.Sort(teams, SortingAlgorithm.PlayerSortByTypes.Goal, true);
            _leagueService = new LeagueService();
            Header.Text = _leagueService.GetAll().First(x => x.TeamIds.Contains(teams.First().Id)).Name.Value;
            
        }

    }
}
