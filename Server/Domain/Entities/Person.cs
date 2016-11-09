using knatteligan.Domain.ValueObjects;

namespace knatteligan.Domain.Entities
{
    public class Person : Entity
    {
        public PersonName Name { get; set; }
        public PersonalNumber PersonalNumber { get; set; }

        public Person() { }

        public Person(PersonName name, PersonalNumber personalNumber)
        {
            Name = name;
            PersonalNumber = personalNumber;
        }

    }
}