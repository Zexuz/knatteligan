using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using knatteligan.Domain.Entities;
using knatteligan.Helpers;
using knatteligan.Services;

namespace KnatteliganWPF
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly LeagueService _leagueService;
        public ObservableCollection<League> Leagues { get; set; }

        public MainWindow()
        {
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

            var scheduleWindow = new SeriesScheduleWindow(currentLeague.Id)
            {
                GameWeeks = currentLeague.MatchWeeks
            };

            var serieScheduleResult = scheduleWindow.ShowDialog();
        }
    }
}