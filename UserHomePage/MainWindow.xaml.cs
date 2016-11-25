using knatteligan.Services;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using knatteligan;
using knatteligan.Domain.Entities;

namespace UserHomePage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly LeagueService _leagueService;
        private readonly TeamService _teamService;
        private readonly SearchService _searchService;
        private TeamWindow _teamWindow;

        public MainWindow()
        {
            InitializeComponent();
            _searchService = new SearchService();
            _leagueService = new LeagueService();

            var leagues = _leagueService.GetAll().ToList();
            LeagueList.ItemsSource = leagues;
        }

        private void ManageLeague_Clicked(object sender, MouseButtonEventArgs e)
        {
            var league = (League)LeagueList.SelectedItem;
            if (league == null) return;
            var leagueWindow = new LeagueWindow(league);
            var addLeagueResult = leagueWindow.ShowDialog();
        }

        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchText = searchTextBox.Text;
            var foundMatch = _searchService.Search(searchText, true);
            SearchList.ItemsSource = foundMatch;
        }

        private void SearchList_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (SearchList.SelectedItem.GetType() == typeof(TeamSearchResultItem))
            {
                var teamObject = ((TeamSearchResultItem) SearchList.SelectedItem).ResultItem;
                var team = (Team) teamObject;
                _teamWindow = new TeamWindow(team.Id);
                _teamWindow.ShowDialog();
            }
            //if (SearchList.SelectedItem.GetType() == typeof(PlayerSearchResultItem))
            //{
            //    var player = ((PlayerSearchResultItem) SearchList.SelectedItem).ResultItem;
            //    player = (Player) player;
            //    _playerStatsWindow = new PlayerStats();
            //}
            else if (SearchList.SelectedItem.GetType() == typeof(LeagueSearchResultItem))
            {
                var leagueObject = ((LeagueSearchResultItem) SearchList.SelectedItem).ResultItem;
                var league = (League) leagueObject;
                 var leagueWindow = new LeagueWindow(league);
                leagueWindow.ShowDialog();
            }


        }

    }

}
