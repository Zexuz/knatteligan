using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        //Fuck wpf, really
        public League League { get; set; }
        public LeagueName LeagueName { get; set; }
        public List<Guid> Teams { get; set; }

        private readonly LeagueService _leagueService;
        private readonly TeamService _teamService;
        private readonly PersonService _personService;

        public CreateLeague()
        {
            InitializeComponent();
            _leagueService = new LeagueService();
            _teamService = new TeamService();
            _personService = new PersonService();
            Teams = new List<Guid>();
            DataContext = this;

            var team1 = new Team(new TeamName("team1"));
            var team2 = new Team(new TeamName("team2"));

            var player1 = new Player(new PersonName("Zlatan", "Ibra"), new PersonalNumber(new DateTime(1996, 6, 6), "4444"), team1);
            var player2 = new Player(new PersonName("Leon", "Lidneberg"), new PersonalNumber(new DateTime(1996, 6, 6), "4444"), team1);

            _personService.Add(player1);
            _personService.Add(player2);


            _teamService.Add(team1);
            _teamService.Add(team2);

            //if (_teamService.GetAllTeams() != null)
            //{
            //    Teams = _teamService.GetAllTeams().Where(x => x.League == League).ToList();
            //}

            TeamList.ItemsSource = new ObservableCollection<Guid>(Teams);
        }


        private void AddTeam_Clicked(object sender, RoutedEventArgs e)
        {
            var addTeam = new AddTeam();
            addTeam.ShowDialog();
        }

        private void AddLeague_Click(object sender, RoutedEventArgs e)
        {
            League = new League(LeagueName, Teams);

            _leagueService.Add(League);

            TeamList.ItemsSource = new ObservableCollection<Guid>(Teams);
        }

        private void CloseCommandHandler_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void RemoveTeam_Click(object sender, RoutedEventArgs e)
        {
            var team = (Team)TeamList.SelectedItem;
            _teamService.RemoveTeam(team);

            //if (_teamService.GetAllTeams() != null)
            //{
            //    Teams = _teamService.GetAllTeams().Where(x => x.League == League).ToList();
            //}

            TeamList.ItemsSource = new ObservableCollection<Guid>(Teams);
        }
    }
}
