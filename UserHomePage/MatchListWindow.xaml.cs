using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using knatteligan.Domain.Entities;
using knatteligan.Helpers;
using knatteligan.Services;
using KnatteliganWPF;

namespace UserHomePage
{
    public partial class MatchListWindow
    {
        public SerializableDictionary<int, MatchWeek> GameWeeks { get; set; }

        private readonly MatchService _matchRepositoryService;

        public MatchListWindow(Guid currentLeagueId)
        {
            GameWeeks = new LeagueService().FindById(currentLeagueId).MatchWeeks;
            _matchRepositoryService = new MatchService();
            InitializeComponent();
            DataContext = this;
        }

        private void MatchListWindowActivated(object sender, EventArgs e)
        {
            Resources["Drinks"] = GameWeeks;
        }

        private void GameWeeksList_Click(object sender, SelectionChangedEventArgs e)
        {
            var currentMatchWeek = (KeyValuePair<int, MatchWeek>)e.AddedItems[0];
            var matches = currentMatchWeek.Value.MatchIds.Select(guid => _matchRepositoryService.FindById(guid));
            CurrentMatchWeekMatches.ItemsSource = new ObservableCollection<Match>(matches);
        }

        private void CurrentMatchWeekMatches_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var listItem = sender as ListBox;
            var match = (Match)listItem.SelectedItems[0];
            var matchProtocol = new MatchProtocol(match);
            matchProtocol.Show();
        }

        private void Back_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Close();
        }

        private void AllMatches_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var matches = _matchRepositoryService.GetAll();
            CurrentMatchWeekMatches.ItemsSource = new ObservableCollection<Match>(matches);
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}