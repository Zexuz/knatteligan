using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using knatteligan.Domain.ValueObjects;
using knatteligan.Helpers;

namespace knatteligan.Domain.Entities
{
    public class League:Entity
    {
        public LeagueName Name { get; set; }
        public List<Guid> Teams { get; set; }
        [XmlIgnore]
        public Dictionary<int, MatchWeek> MatchWeeks { get; set; }
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