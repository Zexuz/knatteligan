using knatteligan.Domain.ValueObjects;

namespace knatteligan.Domain.Entities
{

    public class TeamPerson : Person
    {
        public Team Team { get; set; }

        public TeamPerson() {}

        public TeamPerson(PersonName name, PersonalNumber personalNumber, Team team) : base(name, personalNumber)
        {
            Team = team;
        }

    }

}