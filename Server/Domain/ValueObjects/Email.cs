using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace knatteligan.Domain.ValueObjects
{
    public class Email
    {
        public string Value { get; private set; }
        public Email(string email)
        {
            if (!isEmail(email))
            {
                throw new Exception("Bad Email");
            }
            Value = email;
        }

        private static bool isEmail(string email)
        {
            return Regex.IsMatch(email, @"^[A - Za - z0 - 9._ % +-] +@[A - Za - z0 - 9.-] +\.[A-Za-z]{2,6}$");
        }
    }
}
