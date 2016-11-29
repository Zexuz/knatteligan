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

        public MatchProtocolPage(Match match /*Guid matchID, Guid leagueId*/) //fixa id
        {
            InitializeComponent();
            var teamService = new TeamService();
            var matchEventService = new MatchEventService();

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

        private void MatchProtocolPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}
