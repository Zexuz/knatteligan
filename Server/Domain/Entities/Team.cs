using System;
using System.Collections.Generic;
using knatteligan.Domain.ValueObjects;

namespace knatteligan.Domain.Entities
{
    public class Team:Entity
    {
        //public Guid Id { get; set; }
        public TeamName Name { get; set; }
        public List<TeamPerson> TeamPersons { get; set; }
        
        public Team(TeamName name)
        {
            Name = name;
            TeamPersons = new List<TeamPerson>();
        }
        public Team()
        {

        }
    }
}