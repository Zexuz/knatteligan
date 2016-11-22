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

namespace UserHomePage
{
    /// <summary>
    /// Interaction logic for League.xaml
    /// </summary>
    public partial class League : Window
    {
        private readonly SearchService _searchService;
        private readonly TeamService _teamService;
        public League()
        {
            InitializeComponent();
            _teamService = new TeamService();
            _searchService = new SearchService();
            TeamList.ItemsSource = _teamService.GetAllTeams();
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
            PlayerStats playerStats = new PlayerStats();
            playerStats.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MatchList matchList = new MatchList();
            matchList.ShowDialog();
        }
    }
}
