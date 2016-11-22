﻿using knatteligan.Domain.Entities;
using knatteligan.Services;
using KnatteliganWPF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using KnatteliganWPF;
namespace UserHomePage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        private readonly LeagueService _leagueService;
        public List<League> Leagues { get; set; }
        SearchService searchService;
        public MainWindow()
        {
            InitializeComponent();
            searchService = new SearchService();
             _leagueService = new LeagueService();
            var listLeagues = _leagueService.GetAll().ToList();
            LeagueList.ItemsSource = listLeagues;
         
        }
        private void ManageLeague_Clicked(object sender, MouseButtonEventArgs e) {
          
        }
        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e) {
            var searchText = searchTextBox.Text;
            var foundMatch = searchService.Search(searchText, true);
            SearchList.ItemsSource = foundMatch;
        }

        private void GoToLeague_Click(object sender, RoutedEventArgs e)
        {
            League leagueWindow = new League();
            var addLeagueResult = leagueWindow.ShowDialog();
        }

        private void GoToPlayerStats_Click(object sender, RoutedEventArgs e)
        {
            PlayerStats playerStats = new PlayerStats();
            var playerStatsResult = playerStats.ShowDialog();
        }

        private void GoToMatchList_Click(object sender, RoutedEventArgs e)
        {
            MatchList matchList = new MatchList();
            var matchListResult = matchList.ShowDialog();
        }

        private void GoToSerieSchedule_Click(object sender, RoutedEventArgs e)
        {
            SerieScheduleWindow serieScheduleWindow = new SerieScheduleWindow();
            var serieScheduleResult = serieScheduleWindow.ShowDialog();
        }
    }
}
