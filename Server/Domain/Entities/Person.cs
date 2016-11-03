using System;
using knatteligan.Domain.ValueObjects;

namespace knatteligan.Domain.Entities
{
    public class Person
    {
        public PersonName Name { get; set; }
        public PersonalNumber PersonalNumber { get; set; }
        public Guid Id { get; set; }

        public Person(PersonName name, PersonalNumber personalNumber)
        {
            Name = name;
            PersonalNumber = personalNumber;
            Id = Guid.NewGuid();
        }
        public Person()
        {

        }
    }
}