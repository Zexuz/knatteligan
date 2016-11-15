using System;
using System.Linq;
using knatteligan.Domain.Entities;
using knatteligan.Services;

namespace knatteligan.Helpers
{
    public static class MatchHelper
    {
        public static League GetLeageFromMatchId(Guid matchId)
        {
            foreach (var league in new LeagueService().GetAll().ToList())
            {
                for (var gameWeekIndex = 1; gameWeekIndex < league.MatchWeeks.Count; gameWeekIndex++)
                {
                    var gameWeekMatches = league.MatchWeeks[gameWeekIndex];

                    if (gameWeekMatches.Matches.All(matchGuid => matchGuid != matchId)) continue;

                    return league;
                }
            }

            throw new Exception("Our leage is not in the database");
        }

        public static int GetCurrentMatchWeekNr(League league, Guid matchId)
        {
            for (var gameWeekIndex = 1; gameWeekIndex < league.MatchWeeks.Count; gameWeekIndex++)
            {
                var gameWeekMatches = league.MatchWeeks[gameWeekIndex];

                if (gameWeekMatches.Matches.All(matchGuid => matchGuid != matchId)) continue;

                return gameWeekIndex;
            }

            throw new Exception("Match not found!");
        }

    }

}