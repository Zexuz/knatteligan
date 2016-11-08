using System;
using System.Collections.Generic;
using knatteligan.Domain.ValueObjects;

namespace knatteligan.Domain.Entities
{
    public class Team:Entity
    {
        public TeamName Name { get; set; }
        public List<Guid> TeamPersons { get; set; }
        public List<Guid> Goals { get; set; }

        public Team() {}


        public Team(TeamName name)
        {
            Name = name;
            TeamPersons = new List<Guid>();
            Goals = new List<Guid>();
        }
    }
}