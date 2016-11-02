using System;
using System.Collections.Generic;
using knatteligan.Domain.ValueObjects;

namespace knatteligan.Domain.Entities
{
    public class Team
    {
        public Guid Guid { get; set; }
        public TeamOrLeagueName Name { get; set; }
        public List<TeamPerson> TeamPersons { get; set; }

        public Team(TeamOrLeagueName name)
        {
            Guid = Guid.NewGuid();
            Name = name;
        }
    }
}