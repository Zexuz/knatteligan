using knatteligan.Domain.ValueObjects;

namespace knatteligan.Domain.Entities
{
    public class Player : TeamPerson
    {
        
        public Player(PersonName name, PersonalId personId) : base(name, personId)
        {
        }
    }
}