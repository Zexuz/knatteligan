using knatteligan.Domain.ValueObjects;
using System.Collections.Generic;

namespace knatteligan.Domain.Entities
{
    public class Player : TeamPerson
    {
        public List<YellowCard> YellowCards { get; set; }
        public List<RedCard> RedCards { get; set; }
        public List<Assist> Assists { get; set; }
        

        public Player()
        {

        }

        public Player(PersonName name, PersonalNumber personalNumber, Team team) : base(name, personalNumber, team)
        {
            
        }
    }
}