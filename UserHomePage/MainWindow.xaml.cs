using knatteligan.Services;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace UserHomePage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly LeagueService _leagueService;
        private readonly TeamService _teamService;


        public List<League> Leagues { get; set; }
        SearchService searchService;

        public MainWindow()
        {
            InitializeComponent();
            searchService = new SearchService();
            _leagueService = new LeagueService();
            _teamService = new TeamService();

            var listLeagues = _leagueService.GetAll().ToList();
            LeagueList.ItemsSource = listLeagues;
        }

        private void ManageLeague_Clicked(object sender, MouseButtonEventArgs e)
        {
            var leage = (knatteligan.Domain.Entities.League)LeagueList.SelectedItem;
            if (leage == null) return;
            League leagueWindow = new League(leage);
            var addLeagueResult = leagueWindow.ShowDialog();
        }

        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchText = searchTextBox.Text;
            var foundMatch = searchService.Search(searchText, true);
            SearchList.ItemsSource = foundMatch;
        }
    }
}