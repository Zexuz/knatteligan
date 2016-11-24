using System;
using System.Collections.Generic;
using System.Linq;
using knatteligan.Domain.Entities;
using knatteligan.Repositories;

namespace knatteligan.Services
{
    public class MatchService
    {
        private readonly MatchRepository _matchRepository;

        public MatchService()
        {
            _matchRepository = MatchRepository.GetInstance();
        }

        public IEnumerable<Match> GetAll()
        {
            return _matchRepository.GetAll();
        }

        public Match FindById(Guid id)
        {
            return _matchRepository.FindById(id);
        }

        public void Add(Match match)
        {
            _matchRepository.Add(match);
        }

        public void Save()
        {
            _matchRepository.Save();
        }

        public void SaveMatch(Guid matchId, List<MatchEvent> matchEvents)
        {
            var teamService = new TeamService();
            var personService = new PersonService();

            var match = FindById(matchId);
            SaveMatchEventsFromMatch(match, matchEvents);

            var homeTeamEvents = GetMatchEventsForTeam(match, match.HomeTeamSquadId);
            var awayTeamEvents = GetMatchEventsForTeam(match, match.AwayTeamSquadId);

            var homeGoal = homeTeamEvents.Where(e => e.GetType() == MatchEvents.Goal).ToList();
            var awayGoal = awayTeamEvents.Where(e => e.GetType() == MatchEvents.Goal).ToList();

            var homeTeam = teamService.FindById(match.HomeTeamId);
            var awayTeam = teamService.FindById(match.AwayTeamId);


            if (homeGoal.Count > awayGoal.Count)
            {
                homeTeam.WonMatchIds++;
                awayTeam.LostMatchIds++;
            }
            else if (homeGoal.Count < awayGoal.Count)
            {
                awayTeam.WonMatchIds++;
                homeTeam.LostMatchIds++;
            }
            else
            {
                awayTeam.DrawMatchIds++;
                homeTeam.DrawMatchIds++;
            }

            var players = new List<Player>();
            players.AddRange(homeTeam.PlayerIds.Select(personService.FindPlayerById));
            players.AddRange(awayTeam.PlayerIds.Select(personService.FindPlayerById));

            GetAllCardsAndSuspenPlayers(match, players);
            teamService.Save();
        }

        public void ChangeDate(Guid matchId, DateTime newDate)
        {
            var match = FindById(matchId);
            match.MatchDate = newDate;
            Save();
        }

        private void SaveMatchEventsFromMatch(Match match, List<MatchEvent> matchEvents)
        {
            var personService = new PersonService();
            var teamService = new TeamService();
            var matchEventService = new MatchEventService();
            foreach (var e in matchEvents)
            {
                var person = personService.FindPlayerById(e.PlayerId);
                matchEventService.Add(e);
                match.MatchEventIds.Add(e.Id);
                person.MatchEvents.Add(e.Id);


                if (e.GetType() != MatchEvents.Goal) continue;

                var goal = (Goal) e;
                var team = teamService.FindById(goal.TeamId);
                AddGoalMadeAndConcedToTeams(team, match);
            }

            personService.Save();
        }

        private void AddGoalMadeAndConcedToTeams(Team teamThatMadeTheScore, Match match)
        {
            var teamService = new TeamService();
            teamThatMadeTheScore.GoalsScoredIds++;

            var teamIsAwayTeam = teamThatMadeTheScore.Id == match.AwayTeamId;
            var teamId = teamIsAwayTeam ? match.AwayTeamId : match.HomeTeamId;
            teamService.FindById(teamId).GoalsConcededIds++;
            teamService.Save();
        }

        private List<MatchEvent> GetMatchEventsForTeam(Match match, List<Guid> teamPlayers)
        {
            return match.MatchEventIds
                .Select(new MatchEventService().FindById)
                .Where(mEvent => teamPlayers.Contains(mEvent.PlayerId)).ToList();
        }

        private void GetAllCardsAndSuspenPlayers(Match match, List<Player> listOfAllPlayes)
        {
            var matchEventService = new MatchEventService();
            foreach (var player in listOfAllPlayes)
            {
                var redCards = match.MatchEventIds
                    .Select(eventId => matchEventService.FindById(eventId))
                    .Where(mEvent => mEvent.GetType() == MatchEvents.RedCard && player.Id == mEvent.PlayerId).ToList();
                var yellowCards = match.MatchEventIds
                    .Select(eventId => matchEventService.FindById(eventId))
                    .Where(mEvent => mEvent.GetType() == MatchEvents.YellowCard && player.Id == mEvent.PlayerId)
                    .ToList();

                if (yellowCards.Count == 0 && redCards.Count == 0)
                    continue;

                new MatchWeekService().SetSuspensionLength(yellowCards.Count, redCards.Count, player, match.Id);
            }
        }
    }
}