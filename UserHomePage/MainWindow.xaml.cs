using knatteligan.Services;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using knatteligan.Domain.Entities;

namespace UserHomePage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly LeagueService _leagueService;
        private readonly SearchService _searchService;

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
    }
}