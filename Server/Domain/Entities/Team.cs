using System;
using System.Collections.Generic;
using knatteligan.Domain.ValueObjects;

namespace knatteligan.Domain.Entities
{
    public class Team
    {
        public Guid Id { get; set; }
        public LeagueName Name { get; set; }
        public List<TeamPerson> TeamPersons { get; set; }

        public Team(LeagueName name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }
        public Team()
        {

        }
    }
}