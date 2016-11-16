using System.Collections.ObjectModel;
using System.Diagnostics;
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
        public ObservableCollection<Team> Teams { get; set; }

        private readonly LeagueService _leagueService;
        private readonly TeamService _teamService;
        private readonly PersonService _personService;

        public CreateLeagueWindow()
        {
            InitializeComponent();
            _leagueService = new LeagueService();
            _teamService = new TeamService();
            _personService = new PersonService();
            Teams = new ObservableCollection<Team>();
            TeamList.ItemsSource = Teams;
            DataContext = this;
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

            DialogResult = true;
            Close();
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
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            var team = (Team)TeamList.SelectedItem;
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
    }
}
