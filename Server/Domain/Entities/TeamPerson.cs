using knatteligan.Domain.ValueObjects;

namespace knatteligan.Domain.Entities
{

    public abstract class TeamPerson : Person
    {
        public Team Team { get; set; }

        protected TeamPerson() {}

        protected TeamPerson(PersonName name, PersonalNumber personalNumber, Team team) : base(name, personalNumber)
        {
            Team = team;
        }

    }

}