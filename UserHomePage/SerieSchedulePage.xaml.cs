﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using knatteligan.Domain.Entities;
using knatteligan.Helpers;
using knatteligan.Services;

namespace UserHomePage
{
    /// <summary>
    /// Interaction logic for SerieSchedulePage.xaml
    /// </summary>
    public partial class SerieSchedulePage : Page
    {
        public SerializableDictionary<int, MatchWeek> GameWeeks { get; set; }

        private readonly MatchService _matchRepositoryService;

        public SerieSchedulePage(League league)
        {
            InitializeComponent();
            GameWeeks = league.MatchWeeks;
            _matchRepositoryService = new MatchService();
            HeaderYo.Text = league.Name.Value;
            DataContext = this;
            GameWeeks = league.MatchWeeks;
            _matchRepositoryService = new MatchService();
        }

        private void SerieSchedulePage_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            GameWeeksList.ItemsSource = GameWeeks;
        }

        private void GameWeeksList_Click(object sender, SelectionChangedEventArgs e)
        {
            var currentMatchWeek = (KeyValuePair<int, MatchWeek>)e.AddedItems[0];
            var matches = currentMatchWeek.Value.MatchIds.Select(guid => _matchRepositoryService.FindById(guid)).ToList();
            CurrentMatchWeekMatches.ItemsSource = matches;
        }

        private void CurrentMatchWeekMatches_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var listItem = sender as ListBox;
            if (listItem?.SelectedItems == null || listItem.SelectedItems.Count == 0) return;

            var match = (Match)listItem.SelectedItems[0];
            NavigationService?.Navigate(new MatchProtocolPage(match));
        }

        private void AllMatches_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var matches = _matchRepositoryService.GetAll();
            CurrentMatchWeekMatches.ItemsSource = new ObservableCollection<Match>(matches);
        }
    }
}
