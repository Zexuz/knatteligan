using System;
using System.Collections.Generic;

namespace knatteligan.Domain.Entities
{
    public class Match:Entity
    {
        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }
        public List<Goal> Goals { get; set; }
        public DateTime MatchDate { get; set; }

        public Match(Team homeTeam, Team awayTeam)
        {
            HomeTeam = homeTeam;
            AwayTeam = awayTeam;
        }
        public Match()
        {

        }
    }
}