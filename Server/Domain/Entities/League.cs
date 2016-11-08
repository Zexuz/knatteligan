using System;
using System.Collections.Generic;
using knatteligan.Domain.ValueObjects;

namespace knatteligan.Domain.Entities
{
    public class League:Entity
    {
        public LeagueName Name { get; set; }
        public List<Team> Teams { get; set; }
        public List<Match> Matches { get; set; }
        public DateTime StartDate { get; set; }     

        public League() {}

        public League(LeagueName name, List<Team> teams)
        {
            Name = name;
            Teams = teams;
        }
    }
}