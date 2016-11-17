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
    public partial class SeriesScheduleWindow
    {
        public SerializableDictionary<int, MatchWeek> GameWeeks { get; set; }

        private readonly MatchRepositoryService _matchRepositoryService;
        private readonly LeagueService _leagueService;
        private Guid _currentLeagueId;

        public SeriesScheduleWindow(Guid currentLeagueId)
        {
            _currentLeagueId = currentLeagueId;
            _matchRepositoryService = new MatchRepositoryService();
            InitializeComponent();
            _leagueService = new LeagueService();
            
            DataContext = this;
        }

        private void SeriesScheduleWindowActivated(object sender, EventArgs e)
        {
            Resources["Drinks"] = GameWeeks;
        }

        private void listView_Click(object sender, SelectionChangedEventArgs e)
        {
            Trace.WriteLine("I clicked antoer!");
            var currentMatchWeek = (KeyValuePair<int, MatchWeek>)e.AddedItems[0];

            var matches = currentMatchWeek.Value.Matches.Select(guid => _matchRepositoryService.Find(guid));

            CurrentMatchWeekMatches.ItemsSource = new ObservableCollection<Match>(matches);
        }

        private void CurrentMatchWeekMatches_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            var listItem = sender as ListBox;
            var match = (Match)listItem.SelectedItems[0];
            var matchProtocol = new MatchProtocol(match);
            matchProtocol.Show();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            
            //var currentLeague = _leagueService.FindById(_currentLeagueId);

            var manageLeagueWindow = new CreateLeagueWindow(_currentLeagueId);
            
            manageLeagueWindow.ShowDialog();
        }

    }

}