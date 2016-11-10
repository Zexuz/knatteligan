using System;
using System.Collections.Generic;
using knatteligan.Domain.ValueObjects;
using knatteligan.Helpers;

namespace knatteligan.Domain.Entities
{
    public class League:Entity
    {
        public LeagueName Name { get; set; }
        public List<Team> Teams { get; set; }
        public Dictionary<int, MatchWeek> MatchWeeks { get; set; }
        public DateTime StartDate { get; set; }     

        public League(LeagueName name, List<Team> teams)
        {
            Name = name;
            Teams = teams;
        }
        public League()
        {

        }
    }
}