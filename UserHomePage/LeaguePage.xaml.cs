using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using knatteligan;
using knatteligan.Domain.Entities;
using knatteligan.Services;

namespace UserHomePage
{
    /// <summary>
    /// Interaction logic for LeaguePage.xaml
    /// </summary>
    public partial class LeaguePage : Page
    {
        private readonly League _league;
        private readonly SearchService _searchService;
        private readonly TeamService _teamService;

        public LeaguePage(League league)
        {
            _league = league;
            InitializeComponent();
            DataGrid.AutoGenerateColumns = false;

            _teamService = new TeamService();
            _searchService = new SearchService();
            _league = league;
            DataGrid.ItemsSource = _league.TeamIds.Select(_teamService.FindById);
            LeagueNameTag.Text = _league.Name.Value;
        }

        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchText = SearchTextBox.Text;
            var searchResultItems = _searchService.Search(searchText, true);
            if (string.IsNullOrEmpty(searchText))
            {
                searchResultItems = new List<SearchResultItem>();
            }
            SearchList.ItemsSource = searchResultItems;
        }

        private void Players_Click(object sender, RoutedEventArgs e)
        {
            var teams = _league.TeamIds.Select(_teamService.FindById).ToList();
            NavigationService?.Navigate(new PlayerStatsPage(teams));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new SerieSchedulePage(_league));
        }

        private void DataGrid_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var team = (Team)DataGrid.SelectedItem;
            if (team == null) return;
            NavigationService?.Navigate(new TeamPage(team));
        }


        private void SearchList_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (SearchList.SelectedItems == null || SearchList.SelectedItems.Count == 0) return;

            if (SearchList.SelectedItem.GetType() == typeof(TeamSearchResultItem))
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

        private void DataGrid_OnLoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }
    }
}
