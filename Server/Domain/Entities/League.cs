using System;
using System.Collections.Generic;
using knatteligan.Domain.ValueObjects;

namespace knatteligan.Domain.Entities
{
    public class League : Entity
    {
        public LeagueName Name { get; set; }
        public List<Guid> Teams { get; set; } = new List<Guid>();
        public List<Guid> Matches { get; set; } = new List<Guid>();
        public DateTime StartDate { get; set; }

        public League() { }

        public League(LeagueName name, List<Guid> teams)
        {
            Name = name;
            Teams = teams;
        }

        public override string ToString()
        {
            return Name.Value;
        }
    }
}