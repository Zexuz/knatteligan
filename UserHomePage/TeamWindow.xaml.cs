using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using knatteligan.Domain.Entities;
using knatteligan.Services;

namespace UserHomePage
{
    /// <summary>
    /// Interaction logic for TeamWindow.xaml
    /// </summary>
    public partial class TeamWindow : Window
    {
        private readonly MatchService _matchService;
        private readonly TeamService _teamService;
        private readonly Guid _teamId;
        public TeamWindow(Guid teamId)

        {
            InitializeComponent();
            _teamId = teamId;
            _matchService = new MatchService();
            _teamService = new TeamService();
            var teamMatchList = _matchService.GetAll().Where(m => m.HomeTeamId == teamId || m.AwayTeamId == teamId).OrderBy(m => m.MatchDate);
            TeamMatchList.ItemsSource = teamMatchList;
            TeamNameTxt.Text = _teamService.FindById(teamId).Name.Value;
        }

        private void TeamMatchList_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var listItem = sender as ListBox;
            var match = (Match)listItem.SelectedItems[0];
            var matchProtocol = new MatchProtocol(match);
            matchProtocol.Show();
        }

        private void Players_OnClick(object sender, RoutedEventArgs e)
        {
            var team = _teamService.FindById(_teamId);
            var playerStatsWindow = new PlayerStatsWindow(team);
            playerStatsWindow.ShowDialog();
        }

        private void ButtonBase_OnClick_Back(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
