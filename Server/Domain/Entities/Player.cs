using System;

namespace knatteligan.Domain.Entities
{
    public class Player : TeamPerson
    {
        public Player(string name, DateTime dateOfBirth) : base(name, dateOfBirth)
        {
        }
    }
}