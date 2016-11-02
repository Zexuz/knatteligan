using System;

namespace knatteligan.Domain.Entities
{
    public class Match
    {
        public Guid Guid { get; set; }
        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }
        
        public Match(Team homeTeam, Team awayTeam)
        {
            HomeTeam = homeTeam;
            AwayTeam = awayTeam;
            Guid = new Guid();
        }
    }
}