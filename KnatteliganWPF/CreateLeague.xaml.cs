using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Windows;
using knatteligan.Domain.Entities;
using knatteligan.Domain.ValueObjects;
using knatteligan.Services;

namespace KnatteliganWPF
{
    /// <summary>
    /// Interaction logic for CreateLeague.xaml
    /// </summary>
    public partial class CreateLeague : Window
    {
        public League League { get; set; }
        public LeagueName LeagueName { get; set; }
        public List<Team> Teams { get; set; }

        private readonly LeagueService _leagueService;
        private readonly TeamService _teamService;
        private readonly PersonService _personService;

        public CreateLeague()
        {
            InitializeComponent();
            _leagueService = new LeagueService();
            _teamService = new TeamService();
            _personService = new PersonService();
            Teams = _teamService.GetAllTeams().ToList();
            DataContext = this;

            TeamList.ItemsSource = new ObservableCollection<Team>(Teams);
        }

            var player1 = new Player(new PersonName("Zlatan", "Ibra"), new PersonalNumber(new DateTime(1996, 6, 6), "4444"), team1);
            var player2 = new Player(new PersonName("Leon", "Lidneberg"), new PersonalNumber(new DateTime(1996, 6, 6), "4444"), team1);

        private void AddTeam_Clicked(object sender, RoutedEventArgs e)
        {
            var addTeamWindow = new AddTeamWindow();
            var addTeamResult = addTeamWindow.ShowDialog();
            if (!addTeamResult.HasValue) return;

            if (Teams.Count >= 16)
            {
                AddLeagueButton.IsEnabled = true;
            }

            _teamService.Add(addTeamWindow.Team);
            Teams = _teamService.GetAllTeams().ToList();
            TeamList.ItemsSource = new ObservableCollection<Team>(Teams);

        }
      
        private void AddLeague_Click(object sender, RoutedEventArgs e)
        {
            var teamIds = Teams.Select(x => x.Id).ToList();
            League = new League(LeagueName, teamIds);
            _leagueService.Add(League);
        }

        private void CloseCommandHandler_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void RemoveTeam_Click(object sender, RoutedEventArgs e)
        {
            var team = (Team)TeamList.SelectedItem;
            _teamService.RemoveTeam(team);

            Teams = _teamService.GetAllTeams().ToList();
            TeamList.ItemsSource = new ObservableCollection<Team>(Teams);
        }
    }
}
