using System;
using System.Collections.Generic;
using knatteligan.Domain.ValueObjects;

namespace knatteligan.Domain.Entities
{
    public class League
    {
        public Guid Id { get; set; }
        public LeagueName Name { get; set; }
        public List<Team> Teams { get; set; }
        public List<Match> Matches { get; set; }
        public DateTime StartDate { get; set; }     

        public League(LeagueName name, List<Team> teams)
        {
            Id = Guid.NewGuid();
            Name = name;
            Teams = teams;
        }
        public League()
        {

        }
    }
}