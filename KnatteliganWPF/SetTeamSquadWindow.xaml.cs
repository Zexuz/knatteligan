using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using knatteligan.Domain.Entities;
using knatteligan.Helpers;

namespace KnatteliganWPF
{

    public partial class SetTeamSquadWindow
    {

        public List<Player> StartSquadPlayers { get; set; }

        public ObservableCollection<CheckBox> PlayerList { get; set; }
        private readonly List<Player> _players;


        public SetTeamSquadWindow(List<Player> players, Guid matchId)
        {
            InitializeComponent();

            _players = players;

            var listOfCheckBoxes = players.Select(player => new CheckBox
            {
                Content = player.Name,
                Tag = player.Id,
                IsEnabled = !new MatchService(matchId).IsPlayerSuspended(player.Id)
            }).ToList();

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