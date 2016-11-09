using knatteligan.Domain.ValueObjects;

namespace knatteligan.Domain.Entities
{
    public abstract class Person:Entity
    {
        public PersonName Name { get; set; }
        public PersonalNumber PersonalNumber { get; set; }

        protected Person() {}


        protected Person(PersonName name, PersonalNumber personalNumber)
        {
            Name = name;
            PersonalNumber = personalNumber;
        }

        public new abstract Persons GetType();

    }

    public enum Persons
    {
        Coach,
        Player
    }
}