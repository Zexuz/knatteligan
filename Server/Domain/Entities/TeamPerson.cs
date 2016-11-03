using knatteligan.Domain.ValueObjects;

namespace knatteligan.Domain.Entities
{

    public class TeamPerson : Person
    {

        public TeamPerson() { }

        public TeamPerson(PersonName name, PersonalId personId) : base(name, personId) { }

    }

}