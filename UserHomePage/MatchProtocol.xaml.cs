using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using knatteligan.Domain.Entities;
using knatteligan.Services;

namespace UserHomePage
{
    /// <summary>
    /// Interaction logic for MatchProtocoll.xaml
    /// </summary>
    public partial class MatchProtocol : Window
    {
        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }
        public Match Match { get; set; }
        public List<Player> HomeTeamPlayers { get; set; }
        public List<Player> AwayTeamPlayers { get; set; }

        private readonly MatchEventService _matchEventService;
        private readonly LeagueService _leagueService;
        private readonly TeamService _teamService;
        private readonly PersonService _personService;
        private readonly MatchService _matchService;
        private ListBox _currentFocusedListBox;
        private readonly ObservableCollection<MatchEvent> _matchEventsHome;
        private readonly ObservableCollection<MatchEvent> _matchEventsAway;

        public MatchProtocol(Match match /*Guid matchID, Guid leagueId*/) //fixa id
        {
            InitializeComponent();
            var teamService = new TeamService();
            var matchEventService = new MatchEventService();
            var personService = new PersonService();

            var homeTeamSquad = match.HomeTeamSquadId.Select(personService.FindPlayerById);

            var homeTeamEvents = match.MatchEventIds
                .Select(matchEventService.FindById)
                .Where(mEvent => match.HomeTeamSquadId.Contains(mEvent.PlayerId));

            var awayTeamEvents = match.MatchEventIds
                .Select(matchEventService.FindById)
                .Where(mEvent => match.AwayTeamSquadId.Contains(mEvent.PlayerId));

            var homeGoal = homeTeamEvents.Where(e => e.GetType() == MatchEvents.Goal);
            var awayGoal = awayTeamEvents.Where(e => e.GetType() == MatchEvents.Goal);


            ShowDate.Text = match.MatchDate.ToString("dd-MM-yy");
            //MatchWeek.Text = matchWeek.ToString();
            HomeTeamName.Text = teamService.FindById(match.HomeTeamId).Name.Value;
            AwayTeamName.Text = teamService.FindById(match.AwayTeamId).Name.Value;
            HomeTeamGoals.Text = homeGoal.ToList().Count.ToString();
            AwayTeamGoals.Text = awayGoal.ToList().Count.ToString();
            HomeTeamMatchEvents.ItemsSource = homeTeamEvents;
            AwayTeamMatchEvents.ItemsSource = awayTeamEvents;
        }


        private void Back_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}