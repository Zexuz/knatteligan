using knatteligan.Domain.ValueObjects;
using System.Collections.Generic;

namespace knatteligan.Domain.Entities
{
    public class Player : TeamPerson
    {

        public List<MatchEvent> MatchEvents { get; set; }
        
        public Player() {}

        public Player(PersonName name, PersonalNumber personalNumber, Team team) : base(name, personalNumber, team)
        {
            Assists = new List<Assist>();
            RedCards = new List<RedCard>();
            YellowCards = new List<YellowCard>();
            Goals = new List<Goal>();
        }
    }
}