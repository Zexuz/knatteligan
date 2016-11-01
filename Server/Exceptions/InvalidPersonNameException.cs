using System;

namespace knatteligan.Domain.ValueObjects
{
    public class InvalidPersonNameException : Exception
    {
        public InvalidPersonNameException(string badName) : base(badName)
        {
        }
    }
}