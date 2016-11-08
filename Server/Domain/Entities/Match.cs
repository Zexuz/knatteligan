using System;
using System.Collections.Generic;

namespace knatteligan.Domain.Entities
{
    public class Match:Entity
    {
        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }
        public DateTime MatchDate { get; set; }
        public List<MatchEvent> MatchEvents { get; set; }
        public int MatchWeek { get; set; }

        public Match(Team homeTeam, Team awayTeam)
        {
            Id = new Guid();
            HomeTeam = homeTeam;
            AwayTeam = awayTeam;
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
            return $"{HomeTeam.Name} - {AwayTeam.Name}";
        }
    }
}