using System;
using System.Collections.Generic;

namespace knatteligan.Domain.Entities
{
    public class Match : Entity
    {
        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }
        public DateTime MatchDate { get; set; }
        public List<Guid> MatchEvents { get; set; } = new List<Guid>();

        public Match() { }

        public Match(Team homeTeam, Team awayTeam)
        {
            HomeTeam = homeTeam;
            AwayTeam = awayTeam;
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