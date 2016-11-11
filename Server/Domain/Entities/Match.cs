using System;
using System.Collections.Generic;
using System.Linq;
using knatteligan.Helpers;
using knatteligan.Repositories;

namespace knatteligan.Domain.Entities
{
    public class Match : Entity
    {
        public Guid HomeTeam { get; set; }
        public Guid AwayTeam { get; set; }
        public DateTime MatchDate { get; set; }
        public List<Guid> MatchEvents { get; set; }


        public Match(Guid homeTeam, Guid awayTeam)
        {

            HomeTeam = homeTeam;
            AwayTeam = awayTeam;
        }

        public void SuspendPlayer(Player player, int rounds)
        {
            League league = null;
            var currentMatchWeek = 0;

            var allLeages = LeagueRepository.GetInstance().GetAll().ToList();
            for (int i = 0; i < allLeages.Count(); i++)
            {
                var currentLeage = allLeages[i];

                for (int gameWeekIndex = 1; gameWeekIndex < currentLeage.MatchWeeks.Count; gameWeekIndex++)
                {
                    var gameWeekMatches = currentLeage.MatchWeeks[gameWeekIndex];

                    for (int k = 0; k < gameWeekMatches.Matches.Count; k++)
                    {
                        var matchGuid = gameWeekMatches.Matches[k];

                        if (matchGuid == Id)
                        {
                            league = currentLeage;
                            currentMatchWeek = gameWeekIndex;
                            break;
                        }

                    }

                }

            }

            if (league == null)
                throw new Exception("Our leage is not in the database");

            if (league.MatchWeeks.Count - currentMatchWeek + rounds < 0)
            {
                rounds = league.MatchWeeks.Count - currentMatchWeek;
            }

            for (int i = currentMatchWeek + 1; i < rounds + currentMatchWeek; i++)
            {
                league.MatchWeeks[i].SuspendedPlayers.Add(player.Id);
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

        public Match()
        {
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
    }
}