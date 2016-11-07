using System;
using System.Collections.Generic;
using knatteligan.Domain.ValueObjects;

namespace knatteligan.Domain.Entities
{
    public class Team:Entity
    {
        public LeagueName Name { get; set; }
        public List<TeamPerson> TeamPersons { get; set; }

        public Team(LeagueName name)
        {
            Name = name;
        }
        public Team()
        {

        }
    }
}