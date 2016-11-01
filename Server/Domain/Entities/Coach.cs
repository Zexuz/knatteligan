using System;

namespace knatteligan.Domain.Entities
{
    public class Coach : TeamPerson
    {
        public Coach(string name, DateTime dateOfBirth) : base(name, dateOfBirth)
        {
        }
    }
}