using System;
using System.Collections.Generic;
using knatteligan.Domain.Entities;
using knatteligan.Helpers;

namespace knatteligan.Services
{
    public class MatchWeekService
    {
        public MatchWeekService()
        {
        }

        public bool IsPlayerSuspended(Guid playerId, Guid matchId)
        {
            League _league;
            int _currentMatchWeek;
            SetVariables(out _league, out _currentMatchWeek, matchId);
            return _league.MatchWeeks[_currentMatchWeek].SuspendedPlayers.Contains(playerId);
        }

        public void SuspendPlayer(Player player, int rounds, Guid matchId)
        {
            League _league;
            int _currentMatchWeek;
            SetVariables(out _league, out _currentMatchWeek, matchId);
            if (_league.MatchWeeks.Count - _currentMatchWeek + rounds < 0)
            {
                rounds = _league.MatchWeeks.Count - _currentMatchWeek;
            }

            for (var i = _currentMatchWeek + 1; i < rounds + _currentMatchWeek; i++)
            {
                _league.MatchWeeks[i].SuspendedPlayers.Add(player.Id);
            }
        }

        public void SetSuspensionLength(List<YellowCard> yellowCards, List<RedCard> redCards, Player player,
            Guid matchId)
        {
            if (yellowCards.Count == 2)
            {
                SuspendPlayer(player, 1, matchId);
                return;
            }

            if (redCards.Count == 1)
            {
                SuspendPlayer(player, 3, matchId);
            }
        }

        private static void SetVariables(out League league, out int CurrentMatchWeekNr, Guid matchId)
        {
            league = MatchHelper.GetLeageFromMatchId(matchId);
            CurrentMatchWeekNr = MatchHelper.GetCurrentMatchWeekNr(league, matchId);
        }
    }
}