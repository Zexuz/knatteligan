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

            _matchService = new MatchService();
            _leagueService = new LeagueService();
            _matchEventService = new MatchEventService();
            var homeTeamMatchEvents = new List<MatchEvent>();
            var awayTeamMatchEvents = new List<MatchEvent>();

            var date = _matchService.GetAll().Select(d => d.MatchDate == match.MatchDate);
            //var league = _leagueService.FindById(leagueId); // funkar inte än
            //var matchWeek = MatchWeek

            var homeTeamName = _matchService.GetAll().Where(n => n.HomeTeamId == match.HomeTeamId).Select(n => HomeTeamName).ToString();
            var awayTeamName = _matchService.GetAll().Where(n => n.AwayTeamId == match.AwayTeamId).Select(n => AwayTeamName).ToString();
            var homeTeamScore = _matchService.GetAll().Where(s => s.HomeTeamId == match.HomeTeamId).Select(n => HomeTeamGoals).ToString();
            var awayTeamScore = _matchService.GetAll().Where(s => s.AwayTeamId == match.AwayTeamId).Select(n => AwayTeamGoals).ToString();
            var homeTeamMatchEventIds = _matchService.GetAll().Where(e => e.HomeTeamId == match.HomeTeamId).
                SelectMany(p => p.MatchEventIds).ToList();

            foreach (var homeTeamMatchEventId in homeTeamMatchEventIds)
            {
                homeTeamMatchEvents.Add(_matchEventService.FindById(homeTeamMatchEventId));
            }

            var awayTeamMatchEventIds = _matchService.GetAll().Where(e => e.AwayTeamId == match.AwayTeamId).
               SelectMany(p => p.MatchEventIds).ToList();

            foreach (var awayTeamMatchEventId in awayTeamMatchEventIds)
            {
                homeTeamMatchEvents.Add(_matchEventService.FindById(awayTeamMatchEventId));
            }






            ShowDate.Text = date.ToString();
            //MatchWeek.Text = matchWeek.ToString();
            HomeTeamName.Text = homeTeamName;
            AwayTeamName.Text = awayTeamName;
            HomeTeamGoals.Text = homeTeamScore;
            AwayTeamGoals.Text = awayTeamScore;
            HomeTeamMatchEvents.ItemsSource = homeTeamMatchEvents;
            AwayTeamMatchEvents.ItemsSource = awayTeamMatchEvents;

        }


       

        private void Back_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
