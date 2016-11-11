using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

using knatteligan.Domain.Entities;
using knatteligan.Domain.ValueObjects;
using knatteligan.Repositories;
using knatteligan.Services;

namespace KnatteliganWPF {

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        private readonly LeagueService _leagueService;
        public List<League> Leagues { get; set; }

        public MainWindow() {
            InitializeComponent();
            _leagueService = new LeagueService();
            Leagues = _leagueService.GetAllLeagues().ToList();
            if (Leagues != null) {
                LeagueList.ItemsSource = new ObservableCollection<League>(Leagues);
            }
        }

        private void CreateLeague_Clicked(object sender, RoutedEventArgs e) {
            var createLeagueWindow = new CreateLeagueWindow();
            var createLeagueResult = createLeagueWindow.ShowDialog();
            if (!createLeagueResult.HasValue) return;

            _leagueService.Add(createLeagueWindow.League);

            LeagueList.ItemsSource = new ObservableCollection<League>(Leagues);
        }

        private void ManageLeague_Clicked(object sender, RoutedEventArgs e) {
            var listBoxSender = sender as ListBox;
            var currentLeague = (League) listBoxSender.SelectedItems[0];

            var serieSchedule = new SeriesScheduleWindow {
                GameWeeks = currentLeague.MatchWeeks
            };
            var serieScheduleResult = serieSchedule.ShowDialog();
            //this is a commmenyt!
        }

    }

}