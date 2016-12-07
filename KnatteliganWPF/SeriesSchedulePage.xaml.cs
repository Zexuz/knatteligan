using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using knatteligan.Domain.Entities;
using knatteligan.Helpers;
using knatteligan.Services;

namespace KnatteliganWPF
{
    public partial class SeriesSchedulePage : Page
    {
        public League League { get; set; }
        public SerializableDictionary<int, MatchWeek> GameWeeks { get; set; }
        private readonly MatchService _matchRepositoryService;
        private readonly TeamService _teamService;

        public SeriesSchedulePage(League league)
        {
            InitializeComponent();
            DataContext = this;

            League = league;
            _matchRepositoryService = new MatchService();
            _teamService = new TeamService();
            LeagueNameHeader.Text = League.Name.Value;

        }

        private void SeriesSchedulePage_OnLoaded(object sender, RoutedEventArgs e)
        {
            GameWeeksList.ItemsSource = GameWeeks;
        }

        private void listView_Click(object sender, SelectionChangedEventArgs e)
        {
            var currentMatchWeek = (KeyValuePair<int, MatchWeek>)e.AddedItems[0];
            var matches = currentMatchWeek.Value.MatchIds.Select(guid => _matchRepositoryService.FindById(guid));
            CurrentMatchWeekMatches.ItemsSource = new ObservableCollection<Match>(matches);
        }

        private void CurrentMatchWeekMatches_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var listItem = sender as ListBox;
            if (listItem?.SelectedItems == null || listItem.SelectedItems.Count == 0) return;
            var match = (Match)listItem.SelectedItems[0];

            NavigationService?.Navigate(new MatchProtocolPage(match, League));
        }

        private void ManageLeagueBtn_OnClick(object sender, RoutedEventArgs e)
        {

            var teams = League.TeamIds.Select(_teamService.FindById);
            var teamsOc = new ObservableCollection<Team>(teams);

            NavigationService?.Navigate(new CreateLeaguePage(true, League)
            {
                League = League,
                LeagueName = League.Name,
                Teams = teamsOc
            });
        }
    }
}