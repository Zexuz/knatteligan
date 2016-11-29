using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
        public SerializableDictionary<int, MatchWeek> GameWeeks { get; set; }

        private readonly MatchService _matchRepositoryService;
        private readonly LeagueService _leagueService;
        private readonly Guid _currentLeagueId;

        public SeriesSchedulePage(Guid currentLeagueId)
        {
            InitializeComponent();
            DataContext = this;

            _currentLeagueId = currentLeagueId;
            _matchRepositoryService = new MatchService();
            _leagueService = new LeagueService();
        }

        private void SeriesSchedulePage_OnLoaded(object sender, RoutedEventArgs e)
        {
            GameWeeksList.ItemsSource = GameWeeks;
        }

        private void listView_Click(object sender, SelectionChangedEventArgs e)
        {
            Trace.WriteLine("I clicked antoer!");
            var currentMatchWeek = (KeyValuePair<int, MatchWeek>)e.AddedItems[0];
            var matches = currentMatchWeek.Value.MatchIds.Select(guid => _matchRepositoryService.FindById(guid));
            CurrentMatchWeekMatches.ItemsSource = new ObservableCollection<Match>(matches);
        }

        private void CurrentMatchWeekMatches_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var listItem = sender as ListBox;
            var match = (Match)listItem.SelectedItems[0];
            NavigationService?.Navigate(new MatchProtocolPage(match));
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {

            //var currentLeague = _leagueService.FindById(_currentLeagueId);

            var manageLeagueWindow = new CreateLeagueWindow(_currentLeagueId);

            manageLeagueWindow.ShowDialog();
        }


    }
}