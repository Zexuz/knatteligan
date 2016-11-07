using System;
using System.Collections.Generic;
using knatteligan.Domain.ValueObjects;

namespace knatteligan.Domain.Entities
{
    public class Team
    {
        public Guid Id { get; set; }
        public TeamName Name { get; set; }
        public List<TeamPerson> TeamPersons { get; set; }
        
        public Team(TeamName name)
        {
            Id = Guid.NewGuid();
            Name = name;
            TeamPersons = new List<TeamPerson>();
        }
        public Team()
        {

        }
    }
}