using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using knatteligan;
using knatteligan.Domain.Entities;
using knatteligan.Services;

namespace UserHomePage
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private readonly LeagueService _leagueService;
        private readonly TeamService _teamService;
        private readonly SearchService _searchService;

        public MainPage()
        {
            InitializeComponent();
            _searchService = new SearchService();
            _leagueService = new LeagueService();
            _teamService = new TeamService();

            var leagues = _leagueService.GetAll().ToList();
            LeagueList.ItemsSource = leagues;
        }

        private void ManageLeague_Clicked(object sender, MouseButtonEventArgs e)
        {
            var league = (League)LeagueList.SelectedItem;
            if (league == null) return;
            NavigationService?.Navigate(new LeaguePage(league));
        }

        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchText = searchTextBox.Text;
            var foundMatch = _searchService.Search(searchText, true);
            if (string.IsNullOrEmpty(searchText))
            {
                foundMatch = new List<SearchResultItem>();
            }
            SearchList.ItemsSource = foundMatch;
        }

        private void SearchList_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (SearchList.SelectedItem == null) return;

            if (SearchList.SelectedItem is TeamSearchResultItem)
            {
                var team = (Team)((TeamSearchResultItem)SearchList.SelectedItem).ResultItem;
                NavigationService?.Navigate(new TeamPage(team));
            }

            if (SearchList.SelectedItem is PlayerSearchResultItem)
            {
                var player = (Player)((PlayerSearchResultItem)SearchList.SelectedItem).ResultItem;
                var team = _teamService.FindTeamByPlayerId(player.Id);
                NavigationService?.Navigate(new PlayerStatsPage(team));
            }

            else if (SearchList.SelectedItem.GetType() == typeof(LeagueSearchResultItem))
            {
                var league = (League)((LeagueSearchResultItem)SearchList.SelectedItem).ResultItem;
                NavigationService?.Navigate(new LeaguePage(league));
            }
        }
    }
}
