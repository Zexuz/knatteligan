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
        }

        private void ManageLeague_Clicked(object sender, MouseButtonEventArgs e)
        {
            throw new Exception();
        }

        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchText = SearchTextBox.Text;
            var foundMatch = _searchService.Search(searchText, true);
            if (string.IsNullOrEmpty(searchText))
            {
                foundMatch = new List<SearchResultItem>();
            }
            SearchList.ItemsSource = foundMatch;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var teams = _league.TeamIds.Select(_teamService.FindById).ToList();

            var playerStats = new PlayerStatsWindow(teams);
            var playerStatsResult = playerStats.ShowDialog();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var matchList = new MatchListWindow(_league.Id);
            matchList.ShowDialog();
        }


        private void DataGrid_OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "PlayerIds":
                case "CoachId":
                case "Id":
                case "GoalsConcededIds":
                case "GoalsScoredIds":
                    e.Cancel = true;
                    break;
                case "WonMatchIds":
                    e.Column.Header = "W";
                    break;
                case "LostMatchIds":
                    e.Column.Header = "L";
                    break;
                case "DrawMatchIds":
                    e.Column.Header = "D";
                    break;
                case "DeltaScore":
                    e.Column.Header = "+/-";
                    break;
                case "Points":
                    e.Column.Header = "Pts";
                    break;
                case "GamesPlayedCount":
                    e.Column.Header = "Gp";
                    break;
                case "Name":
                    e.Column.Header = "Team Name";
                    break;
            }
        }

        private void DataGrid_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var team = (Team)DataGrid.SelectedItem;
            var teamWindow = new TeamWindow(team.Id);
            teamWindow.ShowDialog();
        }

        private void DataGrid_OnLoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }


    }
}
