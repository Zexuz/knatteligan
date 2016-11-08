using knatteligan.Domain.ValueObjects;
using System.Collections.Generic;

namespace knatteligan.Domain.Entities
{
    public class Player : TeamPerson
    {
        public List<MatchEvent> MatchEvents { get; set; }
        public int Suspension { get; set; }
        


        public Player(PersonName name, PersonalNumber personalNumber, Team team) : base(name, personalNumber, team)
        {
            MatchEvents = new List<MatchEvent>();
           
        }
    }
}