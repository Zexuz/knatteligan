using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using knatteligan.Domain.Entities;
using knatteligan.Helpers;
using knatteligan.Repositories;

namespace KnatteliganWPF {

    public partial class SeriesScheduleWindow {

        public SerializableDictionary<int, MatchWeek> GameWeeks { get; set; }

        public SeriesScheduleWindow() {
            InitializeComponent();
            Closing += OnWindowClosing;

            DataContext = this;
        }


        public void OnWindowClosing(object sender, CancelEventArgs e) {
            Trace.WriteLine("Shutingdown from Event!");
            Application.Current.Shutdown();
        }

        private void SeriesScheduleWindowActivated(object sender, EventArgs e) {
            Resources["Drinks"] = GameWeeks;
        }


        private void listView_Click(object sender, SelectionChangedEventArgs e) {
            Trace.WriteLine("I clicked antoer!");
            var currentMatchWeek = (KeyValuePair<int, MatchWeek>) e.AddedItems[0];

            var matches = currentMatchWeek.Value.Matches.Select(guid => MatchRepository.GetInstance().Find(guid));

            CurrentMatchWeekMatches.ItemsSource = new ObservableCollection<Match>(matches);
        }

        private void CurrentMatchWeekMatches_OnMouseDoubleClick(object sender, MouseButtonEventArgs e) {

            var matchProtocol = new MatchProtocol();
            matchProtocol.Show();
        }

    }

}