using System;
using System.Collections.Generic;
using knatteligan.Domain.ValueObjects;

namespace knatteligan.Domain.Entities
{
    public class League
    {
        public Guid Guid { get; set; }
        public TeamOrLeagueName Name { get; set; }
        public List<Team> Teams { get; set; }
        public List<Match> Matches { get; set; }
        public DateTime StartDate { get; set; }



        public League()
        {

        }

        public League(TeamOrLeagueName name, List<Team> teams)
        {
            Guid = Guid.NewGuid();
            Name = name;
            Teams = teams;
        }


    }
}