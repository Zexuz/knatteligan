using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using knatteligan.Domain.Entities;
using knatteligan.Helpers;
using knatteligan.Services;

namespace KnatteliganWPF
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private readonly LeagueService _leagueService;
        public ObservableCollection<League> Leagues { get; set; }

        public MainPage()
        {
            Trace.WriteLine(PersonalNumberHelper.GetPersonalTypeForString("961107454831"));
            InitializeComponent();
            _leagueService = new LeagueService();
            Leagues = new ObservableCollection<League>(_leagueService.GetAll().ToList());
            if (Leagues != null)
            {
                LeagueList.ItemsSource = Leagues;
            }
        }

        private void CreateLeague_Clicked(object sender, RoutedEventArgs e)
        {
            var createLeagueWindow = new CreateLeagueWindow();
            var createLeagueResult = createLeagueWindow.ShowDialog();
            if (!createLeagueResult.HasValue) return;

            _leagueService.Add(createLeagueWindow.League);
            Leagues.Add(createLeagueWindow.League);
        }

        private void ManageLeague_Clicked(object sender, RoutedEventArgs e)
        {
            var listBoxSender = sender as ListBox;
            var currentLeague = (League)listBoxSender.SelectedItems[0];

            var seriesSchedulePage = new SeriesSchedulePage(currentLeague.Id);
            seriesSchedulePage.GameWeeks = currentLeague.MatchWeeks;

            NavigationService?.Navigate(seriesSchedulePage);
        }
    }
}
