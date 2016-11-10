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
    /// Interaction logic for CreateLeagueWindow.xaml
    /// </summary>
    public partial class CreateLeagueWindow : Window
    {
        public League League { get; set; }
        public LeagueName LeagueName { get; set; }
        public List<Team> Teams { get; set; }

        private readonly LeagueService _leagueService;
        private readonly TeamService _teamService;
        private readonly PersonService _personService;

        public CreateLeagueWindow()
        {

            InitializeComponent();
            _leagueService = new LeagueService();
            _teamService = new TeamService();
            _personService = new PersonService();
            DataContext = this;
            Teams = _teamService.GetAllTeams().ToList();
            TeamList.ItemsSource = new ObservableCollection<Team>(Teams);

        }


        private void AddTeam_Clicked(object sender, RoutedEventArgs e)
        {
            var addTeamWindow = new AddTeamWindow();
            var addTeamResult = addTeamWindow.ShowDialog();
            if (!addTeamResult.HasValue) return;

            if (Teams.Count >= 16 && Teams.Count % 2 == 0)
            {
                AddLeagueButton.IsEnabled = true;
            }

            _teamService.Add(addTeamWindow.Team);
            Teams.Add(addTeamWindow.Team);
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
            _teamService.Remove(team);
            Teams.Remove(team);
            TeamList.ItemsSource = new ObservableCollection<Team>(Teams);
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            var team = (Team)TeamList.SelectedItem;

            var players = team.PlayerIds.Select(teamPersonId => _personService.FindPlayerById(teamPersonId))
                .Where(teamPerson => teamPerson.GetType() == Persons.Player).Cast<Player>().ToList();

            var addTeamWindow = new AddTeamWindow()
            {
                TeamName = team.Name,
                Team = team
            };
            addTeamWindow.Players = players;
            Console.WriteLine(addTeamWindow.Players);


            var addTeamResult = addTeamWindow.ShowDialog();
            if (!addTeamResult.HasValue) return;

            if (Teams.Count >= 16 && Teams.Count % 2 == 0)
            {
                AddLeagueButton.IsEnabled = true;
            }

            _teamService.Remove(team);
            Teams.Remove(team);


            //TODO: Does this even work?
            _teamService.Edit(addTeamWindow.Team, addTeamWindow.TeamName, addTeamWindow.TeamPersons);
            //Teams.Add(addTeamWindow.Team);
            TeamList.ItemsSource = new ObservableCollection<Team>(Teams);
        }

        private void TeamList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            EditBtn.IsEnabled = true;
            RemoveTeamBtn.IsEnabled = true;
        }
    }
}
