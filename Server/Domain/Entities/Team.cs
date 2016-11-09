using System;
using System.Collections.Generic;
using knatteligan.Domain.ValueObjects;

namespace knatteligan.Domain.Entities
{
    public class Team : Entity
    {
        public TeamName Name { get; set; }
        //TODO: League necessary?
        public List<Guid> TeamPersonIds { get; set; } = new List<Guid>();
        public List<Guid> Goals { get; set; } = new List<Guid>();

        public Team() { }


        public Team(TeamName name, IEnumerable<TeamPerson> teamPersons)
        {
            Name = name;
            foreach (var teamPersonId in teamPersons)
            {
                TeamPersonIds.Add(teamPersonId.Id);
            }
        }
        public override string ToString()
        {
            return Name.Value;
        }
    }
}