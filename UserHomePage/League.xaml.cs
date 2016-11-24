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
using System.Windows.Shapes;
using knatteligan.Services;
using knatteligan.Domain.Entities;

namespace UserHomePage
{
    /// <summary>
    /// Interaction logic for League.xaml
    /// </summary>
    public partial class League : Window
    {
        private readonly knatteligan.Domain.Entities.League _league;

        private readonly SearchService _searchService;
        private readonly TeamService _teamService;

        public League(knatteligan.Domain.Entities.League league)
        {
            _league = league;
            InitializeComponent();
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
            SearchList.ItemsSource = foundMatch;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var teamSelected = (Team) DataGrid.SelectedItem;
            if (teamSelected == null) return;

            PlayerStats playerStats = new PlayerStats(teamSelected);
            playerStats.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MatchListWindow matchList = new MatchListWindow(_league.Id);
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
    }
}