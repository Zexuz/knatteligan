using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using knatteligan.Domain.Entities;
using knatteligan.Services;

namespace UserHomePage
{
    /// <summary>
    /// Interaction logic for TeamPage.xaml
    /// </summary>
    public partial class TeamPage : Page
    {
        private readonly MatchService _matchService;
        private readonly TeamService _teamService;
        private readonly Team _team;

        public TeamPage(Team team)

        {
            InitializeComponent();
            _team = team;
            _matchService = new MatchService();
            _teamService = new TeamService();
            var teamMatches = _matchService.GetAll().Where(m => m.HomeTeamId == team.Id || m.AwayTeamId == team.Id).OrderBy(m => m.MatchDate);
            TeamMatchList.ItemsSource = teamMatches;
            TeamNameTxt.Text = _teamService.FindById(team.Id).Name.Value;
        }

        private void TeamMatchList_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var listItem = sender as ListBox;
            if (listItem?.SelectedItems == null || listItem.SelectedItems.Count == 0) return;
            var match = (Match)listItem.SelectedItems[0];
            NavigationService?.Navigate(new MatchProtocolPage(match));
        }

        private void Players_OnClick(object sender, RoutedEventArgs e)
        {
            var team = _teamService.FindById(_team.Id);
            NavigationService?.Navigate(new PlayerStatsPage(team));
        }

    }
}
