using System.Text.RegularExpressions;

using knatteligan.CustomExceptions;

namespace knatteligan.Domain.ValueObjects
{
    public class Email
    {
        public string Value { get; private set; }
        public Email(string email)
        {
            if (!IsEmail(email))
            {
                throw new InvalidEmailException("Bad Email");
            }
            Value = email;
        }

        public Email()
        {
            
        }

        private static bool IsEmail(string email)
        {
            return Regex.IsMatch(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        }

        public override string ToString()
        {
            return Value;
        }

    }
}
