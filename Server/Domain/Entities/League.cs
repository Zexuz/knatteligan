using System;
using System.Collections.Generic;
using knatteligan.Domain.ValueObjects;
using knatteligan.Helpers;

namespace knatteligan.Domain.Entities
{
    public class League : Entity
    {
        public LeagueName Name { get; set; }
        public List<Guid> TeamIds { get; set; }
        public SerializableDictionary<int, MatchWeek> MatchWeeks { get; set; }
        public DateTime StartDate { get; set; }

        public League() { }

        public League(LeagueName name, List<Guid> teamIds)
        {
            Name = name;
            TeamIds = teamIds;
        }

        public override string ToString()
        {
            return Name.Value;
        }
    }
}