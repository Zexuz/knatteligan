using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using knatteligan.Domain.Entities;
using knatteligan.Domain.ValueObjects;
using knatteligan.Services;
using knatteligan.Helpers;

namespace KnatteliganWPF
{
    /// <summary>
    /// Interaction logic for CreateLeaguePage.xaml
    /// </summary>
    public partial class CreateLeaguePage : Page
    {
        public League League { get; set; }
        public LeagueName LeagueName { get; set; }
        public ObservableCollection<Team> Teams { get; set; }

        private readonly LeagueService _leagueService;
        private readonly TeamService _teamService;
        private readonly PersonService _personService;


        public CreateLeaguePage()
        {
            InitializeComponent();
            _leagueService = new LeagueService();
            _teamService = new TeamService();
            _personService = new PersonService();
            Teams = new ObservableCollection<Team>();
            TeamList.ItemsSource = Teams;
            DataContext = this;
        }

        public CreateLeaguePage(Guid currentLeagueId)
        {
            _leagueService = new LeagueService();
            _teamService = new TeamService();
            _personService = new PersonService();
            var league = _leagueService.FindById(currentLeagueId);
            LeagueName = league.Name;
            Teams = new ObservableCollection<Team>();

            foreach (var teamId in league.TeamIds)
            {
                Teams.Add(_teamService.FindById(teamId));
            }

            InitializeComponent();
            TeamList.ItemsSource = new ObservableCollection<Team>(Teams);
            leagueName.Text = LeagueName.Value;
            //TODO LeagueName does not show in textbox
        }


        private void AddTeam_Clicked(object sender, RoutedEventArgs e)
        {
            var addTeamWindow = new AddTeamWindow(false);
            var windowRes = addTeamWindow.ShowDialog();

            Trace.WriteLine(windowRes);
            if (windowRes.HasValue && !windowRes.Value)
            {
                Trace.WriteLine("We did not press the add button");
                return;
            }

            if (addTeamWindow.Team == null)
            {
                Trace.WriteLine("Team is null");
                return;
            }

            if (addTeamWindow.Coach == null)
            {
                Trace.WriteLine("Coach is null");
                return;
            }

            _teamService.Add(addTeamWindow.Team);
            _personService.Add(addTeamWindow.Coach);
            Teams.Add(addTeamWindow.Team);
        }

        private void AddLeague_Click(object sender, RoutedEventArgs e)
        {
            var teamIds = Teams.Select(x => x.Id).ToList();
            League = new League(LeagueName, teamIds);
            var newSerie = new CreateSeriesSchedule().GetFullSeries(Teams.ToList());
            League.MatchWeeks = newSerie;
            
            MainPage.Leagues.Add(League);
            _leagueService.Add(League);
            NavigationService.GoBack();
        }

        private void RemoveTeam_Click(object sender, RoutedEventArgs e)
        {
            var team = (Team)TeamList.SelectedItem;
            if(team== null) return;

            _teamService.Remove(team);
            Teams.Remove(team);
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            var team = (Team)TeamList.SelectedItem;
            if(team== null) return;

            var coach = _personService.FindCoachById(team.CoachId);
            var players = team.PlayerIds.Select(teamPersonId => _personService.FindPlayerById(teamPersonId));
            var playerOc = new ObservableCollection<Player>(players);

            var addTeamWindow = new AddTeamWindow(true)
            {
                TeamName = team.Name,
                Team = team,
                Players = playerOc,
                Coach = coach,
                PersonName = coach.Name,
                PersonalNumber = coach.PersonalNumber,
                PhoneNumber = coach.PhoneNumber,
                Email = coach.Email
            };

            var addTeamResult = addTeamWindow.ShowDialog();
            if (!addTeamResult.HasValue) return;

            _teamService.Edit(addTeamWindow.Team, addTeamWindow.TeamName, addTeamWindow.Players, addTeamWindow.Coach);
            //TODO: Replace this hack
            Teams.Remove(addTeamWindow.Team);
            Teams.Add(addTeamWindow.Team);
        }

        private void CloseCommandHandler_Click(object sender, RoutedEventArgs e)
        {
            
            if (leagueName.Text.Length > 0 || Teams.Count > 0)
            {
                var result = MessageBox.Show("Are you sure you want to cancel?", "Message", MessageBoxButton.YesNo);              
                switch (result)
                { 
                    case MessageBoxResult.Yes:
                        leagueName.Text = "";
                        foreach (var team in Teams)
                        {
                            _teamService.Remove(team);
                        }
                        Teams = new ObservableCollection<Team>();
                        TeamList.ItemsSource = Teams;
                         
                        NavigationService.GoBack();
                        break;
                    case MessageBoxResult.No:
                        break;        
                }
                
            }
            else NavigationService.GoBack();
        }
    }
}
