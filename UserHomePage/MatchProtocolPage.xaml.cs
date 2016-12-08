using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using knatteligan.Domain.Entities;
using knatteligan.Services;

namespace UserHomePage
{
    /// <summary>
    /// Interaction logic for MatchProtocolPage.xaml
    /// </summary>
    public partial class MatchProtocolPage : Page
    {
        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }
        public Match Match { get; set; }
        public List<Player> HomeTeamPlayers { get; set; }
        public List<Player> AwayTeamPlayers { get; set; }
        private readonly LeagueService _leagueService;

        public MatchProtocolPage(Match match) 
        {
            InitializeComponent();
            _leagueService = new LeagueService();

            var league = _leagueService.GetAll().First(x => x.TeamIds.Contains(match.HomeTeamId));
            LeagueName.Text = league.Name.Value;
            var teamService = new TeamService();
            var matchEventService = new MatchEventService();

            var homeTeamEvents = match.MatchEventIds
                .Select(matchEventService.FindById)
                .Where(mEvent => match.HomeTeamSquadId.Contains(mEvent.PlayerId));

            var awayTeamEvents = match.MatchEventIds
                .Select(matchEventService.FindById)
                .Where(mEvent => match.AwayTeamSquadId.Contains(mEvent.PlayerId));

            var homeTeamGoals = homeTeamEvents.Where(e => e.GetType() == MatchEvents.Goal);
            var awayTeamGoals = awayTeamEvents.Where(e => e.GetType() == MatchEvents.Goal);


            ShowDate.Text = match.MatchDate.ToString("dd-MM-yy");
            //MatchWeek.Text = matchWeek.ToString();
            HomeTeamName.Text = teamService.FindById(match.HomeTeamId).Name.Value;
            AwayTeamName.Text = teamService.FindById(match.AwayTeamId).Name.Value;
            HomeTeamGoals.Text = homeTeamGoals.ToList().Count.ToString();
            AwayTeamGoals.Text = awayTeamGoals.ToList().Count.ToString();
            HomeTeamMatchEvents.ItemsSource = homeTeamEvents;
            AwayTeamMatchEvents.ItemsSource = awayTeamEvents;
        }
    }
}
