using System;

namespace knatteligan.Domain.Entities
{
    public class TeamPerson : Person
    {
        public TeamPerson(string name, DateTime dateOfBirth) : base(name, dateOfBirth)
        {
        }
    }
}