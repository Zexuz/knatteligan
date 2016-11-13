using System;
using System.Collections.Generic;

using knatteligan.Domain.Entities;
using knatteligan.Repositories;

namespace knatteligan.Helpers {

    public class MatchService {

        private readonly League _league;
        private readonly int _currentMatchWeek;
        private readonly Match _match;

        public MatchService(Guid matchId) {
            _match = MatchRepository.GetInstance().Find(matchId);

            _league = MatchHelper.GetLeageFromMatchId(matchId);
            _currentMatchWeek = MatchHelper.GetCurrentMatchWeekNr(_league, matchId);
        }


        public bool IsPlayerSuspended(Guid playerId) {
            return _league.MatchWeeks[_currentMatchWeek].SuspendedPlayers.Contains(playerId);
        }

        public void SuspendPlayer(Player player, int rounds, Guid matchId) {
            var league = GetLeageFromMatchId(matchId);
            var currentMatchWeekNr = GetCurrentMatchWeekNr(league, matchId);

            if (league.MatchWeeks.Count - currentMatchWeekNr + rounds < 0) {
                rounds = league.MatchWeeks.Count - currentMatchWeekNr;
            }

            for (var i = currentMatchWeekNr + 1; i < rounds + currentMatchWeekNr; i++) {
                league.MatchWeeks[i].SuspendedPlayers.Add(player.Id);
            }
        }

        public void SetSuspensionLength(List<YellowCard> yellowCards, List<RedCard> redCards, Player player) {
            if (yellowCards.Count == 2) {
                SuspendPlayer(player, 1, _match.Id);
                return;
            }


            if (redCards.Count == 1) {
                SuspendPlayer(player, 3, _match.Id);
            }
        }

    }

}