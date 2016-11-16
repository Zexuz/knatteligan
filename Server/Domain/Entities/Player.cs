using System;
using knatteligan.Domain.ValueObjects;
using System.Collections.Generic;

namespace knatteligan.Domain.Entities
{
    public class Player : Person
    {
        public List<Guid> MatchEvents { get; set; }

        public Player() { }

        public Player(PersonName name, PersonalNumber personalNumber) : base(name, personalNumber)
        {
            MatchEvents = new List<Guid>();
        }

        public override Persons GetType()
        {
            return Persons.Player;
        }

        public override string ToString()
        {
            return Name.Name;
        }
    }
}