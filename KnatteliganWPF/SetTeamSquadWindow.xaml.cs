using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

using knatteligan.Domain.Entities;

namespace KnatteliganWPF {

    public partial class SetTeamSquadWindow{

        public List<Player> StartSquadPlayers { get; set; }

        public ObservableCollection<CheckBox> PlayerList {get; set ;}
        private readonly List<Player> _players;


        public SetTeamSquadWindow(List<Player> players) {
            InitializeComponent();

            _players = players;

            var listOfCheckBoxes = new List<CheckBox>();

            foreach (var player in players) {
                var checkBox = new CheckBox();
                checkBox.Content = player.Name;
                checkBox.Tag = player.Id;
                listOfCheckBoxes.Add(checkBox);
            }

            PlayerList = new ObservableCollection<CheckBox>(listOfCheckBoxes);
            DataContext =this;
        }




        private void WindowActivated(object sender, EventArgs e) {
            Resources["Players"] = _players;
        }

        private void Add_OnClick(object sender, RoutedEventArgs e) {
            DialogResult = true;
            Close();
        }

        private void Cancel_OnClick(object sender, RoutedEventArgs e) {
            DialogResult = false;
            Close();
        }

    }



}