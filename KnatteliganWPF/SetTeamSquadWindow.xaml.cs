using System;
using System.Collections.Generic;

using knatteligan.Domain.Entities;

namespace KnatteliganWPF {

    public partial class SetTeamSquadWindow{

        public List<Player> StartSquadPlayers { get; set; }

        private readonly List<Player> _players;

        public SetTeamSquadWindow(List<Player> players) {
            InitializeComponent();

            _players = players;

            DataContext = this;
        }


        private void WindowActivated(object sender, EventArgs e) {
            Resources["Players"] = _players;
        }

    }

}