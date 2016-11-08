using System;

using knatteligan.Domain.ValueObjects;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace knatteligan.Domain.Entities
{
    public class Player : TeamPerson
    {

        public List<Guid> MatchEvents { get; set; }
        
        public Player() {}

        public Player(PersonName name, PersonalNumber personalNumber, Team team) : base(name, personalNumber, team)
        {
           MatchEvents = new List<Guid>();
        }
    }
}