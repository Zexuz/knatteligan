using System;

namespace knatteligan.Domain.Entities
{
    public class Person
    {
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Guid Id { get; set; }

        public Person(string name, DateTime dateOfBirth)
        {
            Name = name;
            DateOfBirth = dateOfBirth;
            Id = Guid.NewGuid();
        }
    }
}