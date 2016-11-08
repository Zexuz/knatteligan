using System;
using System.Collections.Generic;
using knatteligan.Domain.ValueObjects;

namespace knatteligan.Domain.Entities
{
    public class Team : Entity
    {
        public Guid Id { get; set; }
        public TeamName Name { get; set; }
        public List<TeamPerson> TeamPersons { get; set; }
        //TODO: Goals?

        //Test
        public League League { get; set; }

        public Team(TeamName name)
        {
            Name = name;
            TeamPersons = new List<TeamPerson>();
            Id = Guid.NewGuid();
        }
        public Team()
        {

        }

        public override string ToString()
        {
            return Name.Value;
        }
    }
}