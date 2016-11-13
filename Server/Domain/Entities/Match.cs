using System;
using System.Collections.Generic;
using System.Linq;
using knatteligan.Repositories;

namespace knatteligan.Domain.Entities
{
    public class Match : Entity
    {
        public Guid HomeTeam { get; set; }
        public Guid AwayTeam { get; set; }
        public DateTime MatchDate { get; set; }
        public List<Guid> MatchEvents { get; set; }

        public List<Guid> HomeTeamSquad;
        public List<Guid> AwayTeamSquad;


        private League _league;
        private int _currentMatchWeekNr;

        public Match(){}

        public Match(Guid homeTeam, Guid awayTeam)
        {

            HomeTeam = homeTeam;
            AwayTeam = awayTeam;

            HomeTeamSquad = new List<Guid>();
            AwayTeamSquad = new List<Guid>();

            SetLeagueAndCurrentMatchWeekNr();
        }

        public bool IsPlayerSuspended(Guid playerId) {
            return _league.MatchWeeks[_currentMatchWeekNr].SuspendedPlayers.Contains(playerId);
        }

        public void SuspendPlayer(Player player, int rounds)
        {

            if (_league.MatchWeeks.Count - _currentMatchWeekNr+ rounds < 0)
            {
                rounds = _league.MatchWeeks.Count - _currentMatchWeekNr;
            }

            for (int i = _currentMatchWeekNr+ 1; i < rounds + _currentMatchWeekNr; i++)
            {
                _league.MatchWeeks[i].SuspendedPlayers.Add(player.Id);
            }


        }

        public void SetSuspensionLength(List<YellowCard> yellowCards, List<RedCard> redCards, Player player)
        {
            if (yellowCards.Count == 2)
            {
                SuspendPlayer(player, 1);
                return;
            }


            if (redCards.Count == 1)
            {
                SuspendPlayer(player, 3);
            }


        }

        public void Swap()
        {
            var tempTeam = HomeTeam;
            HomeTeam = AwayTeam;
            AwayTeam = tempTeam;
        }

        public override string ToString()
        {
            return $"{TeamRepository.GetInstance().Find(HomeTeam).Name} - {TeamRepository.GetInstance().Find(HomeTeam).Name}";
        }

        private void SetLeagueAndCurrentMatchWeekNr() {
            var allLeages = LeagueRepository.GetInstance().GetAll().ToList();
            for (int i = 0; i < allLeages.Count(); i++)
            {
                var currentLeage = allLeages[i];

                for (int gameWeekIndex = 1; gameWeekIndex < currentLeage.MatchWeeks.Count; gameWeekIndex++) {
                    var gameWeekMatches = currentLeage.MatchWeeks[gameWeekIndex];

                    if (gameWeekMatches.Matches.All(matchGuid => matchGuid != Id)) continue;

                    _league = currentLeage;
                    _currentMatchWeekNr = gameWeekIndex;
                }

            }

            if (_league == null)
                throw new Exception("Our leage is not in the database");
        }

    }
}