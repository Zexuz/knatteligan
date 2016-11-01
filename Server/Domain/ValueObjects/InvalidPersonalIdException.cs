using System;

namespace knatteligan.Domain.ValueObjects
{
    public class InvalidPersonalIdException : Exception
    {
        public InvalidPersonalIdException(string str) : base(str)
        {
        }
    }
}