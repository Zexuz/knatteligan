using System;
using knatteligan.Domain.ValueObjects;

namespace knatteligan.Domain.Entities
{
    public class Person
    {
        public PersonName Name { get; set; }
        public PersonalId PersonId { get; set; }
        public Guid Id { get; set; }

        public Person(PersonName name, PersonalId personId)
        {
            Name = name;
            PersonId = personId;
            Id = Guid.NewGuid();
        }
    }
}