using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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

            var homeTeam = teamService.FindById(match.HomeTeamId);
            var awayTeam = teamService.FindById(match.AwayTeamId);

            RemoveMatchEventsFromMatchAndTeams(match, awayTeam, homeTeam);


            SaveMatchEventsFromMatch(match, matchEvents);

            var homeTeamEvents = GetMatchEventsForTeam(match, match.HomeTeamSquadId);
            var awayTeamEvents = GetMatchEventsForTeam(match, match.AwayTeamSquadId);

            var homeGoal = homeTeamEvents.Where(e => e.GetType() == MatchEvents.Goal).ToList();
            var awayGoal = awayTeamEvents.Where(e => e.GetType() == MatchEvents.Goal).ToList();


            if (homeGoal.Count > awayGoal.Count)
            {
                homeTeam.WonMatchIds.Add(matchId);
                awayTeam.LostMatchIds.Add(matchId);
            }
            else if (homeGoal.Count < awayGoal.Count)
            {
                awayTeam.WonMatchIds.Add(matchId);
                homeTeam.LostMatchIds.Add(matchId);
            }
            else
            {
                awayTeam.DrawMatchIds.Add(matchId);
                homeTeam.DrawMatchIds.Add(matchId);
            }

            var players = new List<Player>();
            players.AddRange(homeTeam.PlayerIds.Select(personService.FindPlayerById));
            players.AddRange(awayTeam.PlayerIds.Select(personService.FindPlayerById));

            GetAllCardsAndSuspenPlayers(match, players);
            teamService.Save();
            Save();
        }

        public void ChangeDate(Guid matchId, DateTime newDate)
        {
            var match = FindById(matchId);
            match.MatchDate = newDate;
            Save();
        }

        public void SetStartSquad(Guid matchId, bool homeTeam, List<Guid> players)
        {
            var match = FindById(matchId);

            if (homeTeam) match.HomeTeamSquadId = players;
            else match.AwayTeamSquadId = players;
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
                AddGoalMadeAndConcedToTeams(team, match, goal);
            }

            personService.Save();
        }

        private void AddGoalMadeAndConcedToTeams(Team teamThatMadeTheScore, Match match, Goal goal)
        {
            var teamService = new TeamService();
            teamThatMadeTheScore.GoalsScoredIds.Add(goal.Id);

            var teamIsAwayTeam = teamThatMadeTheScore.Id == match.AwayTeamId;
            var teamId = teamIsAwayTeam ? match.AwayTeamId : match.HomeTeamId;
            teamService.FindById(teamId).GoalsConcededIds.Add(goal.Id);
            teamService.Save();
        }

        private List<MatchEvent> GetMatchEventsForTeam(Match match, List<Guid> teamPlayers)
        {
            return match.MatchEventIds
                .Select(new MatchEventService().FindById)
                .Where(mEvent => teamPlayers.Contains(mEvent.PlayerId)).ToList();
        }

        private List<Goal> GetGoalsForTeam(Match match, Team team)
        {
            return GetMatchEventsForTeam(match, team.PlayerIds)
                .Where(e => e.GetType() == MatchEvents.Goal)
                .Select(g => (Goal) g)
                .ToList();
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

        private void RemoveMatchEventsFromMatchAndTeams(Match match, Team awayTeam, Team homeTeam)
        {
            var homeGoals = GetGoalsForTeam(match, homeTeam);
            var awayGoals = GetGoalsForTeam(match, awayTeam);

            foreach (var homeGoal in homeGoals)
            {
                homeTeam.GoalsScoredIds.Remove(homeGoal.Id);
                awayTeam.GoalsConcededIds.Remove(homeGoal.Id);
            }

            foreach (var awayGoal in awayGoals)
            {
                awayTeam.GoalsScoredIds.Remove(awayGoal.Id);
                homeTeam.GoalsConcededIds.Remove(awayGoal.Id);
            }

            RemoveMatchEventsFromPlayersInMatch(match);

            if (homeGoals.Count > awayGoals.Count)
            {
                homeTeam.WonMatchIds.Remove(match.Id);
                awayTeam.LostMatchIds.Remove(match.Id);
            }
            else if (homeGoals.Count < awayGoals.Count)
            {
                awayTeam.WonMatchIds.Remove(match.Id);
                homeTeam.LostMatchIds.Remove(match.Id);
            }
            else
            {
                awayTeam.DrawMatchIds.Remove(match.Id);
                homeTeam.DrawMatchIds.Remove(match.Id);
            }

            match.MatchEventIds = new List<Guid>();
            match.HomeTeamSquadId = new List<Guid>();
            match.AwayTeamSquadId = new List<Guid>();
        }

        private void RemoveMatchEventsFromPlayersInMatch(Match match)
        {
            var personSerivce = new PersonService();
            var matchEventService = new MatchEventService();
            var matchWeekService = new MatchWeekService();


            foreach (var matchEventId in match.MatchEventIds)
            {
                var mEvent = matchEventService.FindById(matchEventId);
                var player = personSerivce.FindPlayerById(mEvent.PlayerId);

                var matchEvents = player.MatchEvents
                    .Select(matchEventService.FindById)
                    .Where(e => e.MatchId == match.Id);


                var yellowCards = 0;
                foreach (var matchEvent in matchEvents)
                {
                    var type = matchEvent.GetType();

                    if (type == MatchEvents.RedCard) matchWeekService.RemoveSuspension(3, player, match.Id);
                    if (type == MatchEvents.YellowCard) yellowCards++;

                    player.MatchEvents.Remove(matchEvent.Id);
                }

                if (yellowCards > 1) matchWeekService.RemoveSuspension(1, player, match.Id);
            }

            personSerivce.Save();
        }
    }
}