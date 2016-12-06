using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using knatteligan.Domain.Entities;
using knatteligan.Services;
using MahApps.Metro.Controls;

namespace KnatteliganWPF
{
    public partial class SetTeamSquadWindow : MetroWindow
    {
        public List<Player> StartSquadPlayers { get; set; }

        public ObservableCollection<CheckBox> PlayerList { get; set; }
        private readonly List<Player> _players;

        public SetTeamSquadWindow(List<Player> players, Guid matchId, List<Guid> alreadySetPlayersId)
        {
            InitializeComponent();
            _players = players;

            if(alreadySetPlayersId == null)
                alreadySetPlayersId = new List<Guid>();

            Trace.WriteLine(alreadySetPlayersId.Count);

            var listOfCheckBoxes = new List<CheckBox>();
            foreach (var player in players)
            {
                var playerIsSuspenden = new MatchWeekService().IsPlayerSuspended(player.Id, matchId);
                var checkBox = new CheckBox();
                checkBox.Content = player.Name;
                checkBox.Tag = player.Id;
                checkBox.IsChecked = alreadySetPlayersId.Contains(player.Id);
                checkBox.IsEnabled = !(alreadySetPlayersId.Contains(player.Id) || playerIsSuspenden);
                listOfCheckBoxes.Add(checkBox);
            }


            PlayerList = new ObservableCollection<CheckBox>(listOfCheckBoxes);
            DataContext = this;
        }

        private void WindowActivated(object sender, EventArgs e)
        {
            Resources["Players"] = _players;
        }

        private void Add_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void Cancel_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}