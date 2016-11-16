using System;
using knatteligan.Domain.Entities;
using knatteligan.Helpers;
using knatteligan.Repositories;

namespace knatteligan.Services
{
    public class MatchWeekService
    {
        public MatchWeekService()
        {
        }

        public bool IsPlayerSuspended(Guid playerId, Guid matchId)
        {
            League league;
            int currentMatchWeek;
            SetVariables(out league, out currentMatchWeek, matchId);

            return league.MatchWeeks[currentMatchWeek].SuspendedPlayers.Contains(playerId);
        }

        public void SuspendPlayer(Player player, int rounds, Guid matchId)
        {
            League league;
            int currentMatchWeek;
            SetVariables(out league, out currentMatchWeek, matchId);

            if (league.MatchWeeks.Count - currentMatchWeek + rounds < 0)
            {
                rounds = league.MatchWeeks.Count - currentMatchWeek;
            }

            for (var i = currentMatchWeek + 1; i < rounds + currentMatchWeek; i++)
            {
                league.MatchWeeks[i].SuspendedPlayers.Add(player.Id);
            }

            LeagueRepository.GetInstance().Save();
        }

        public void SetSuspensionLength(int yellowCards, int redCards, Player player, Guid matchId)
        {
            if (yellowCards >= 2)
            {
                SuspendPlayer(player, 1, matchId);
                return;
            }

            if (redCards >= 1)
            {
                SuspendPlayer(player, 3, matchId);
            }
        }

        private static void SetVariables(out League league, out int currentMatchWeekNr, Guid matchId)
        {
            league = MatchHelper.GetLeageFromMatchId(matchId);
            currentMatchWeekNr = MatchHelper.GetCurrentMatchWeekNr(league, matchId);
        }
    }
}