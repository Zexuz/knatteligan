using System;
using System.Collections.Generic;
using knatteligan.Domain.ValueObjects;

namespace knatteligan.Domain.Entities
{
    public class Team:Entity
    {
        public TeamName Name { get; set; }
        //TODO: League necessary?
        public List<Guid> TeamPersons { get; set; } = new List<Guid>();
        public List<Guid> Goals { get; set; } = new List<Guid>();

        public Team() { }


        public Team(TeamName name)
        {
            Name = name;
        }
        public override string ToString()
        {
            return Name.Value;
        }
    }
}