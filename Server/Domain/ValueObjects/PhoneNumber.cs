using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace knatteligan.Domain.ValueObjects
{
    public class PhoneNumber
    {
        public string Value { get; private set; }

        public PhoneNumber (string number)
        {
            if (!isPhoneNumber(number))
            {
                throw new Exception("Bad phone number");   
            }
            Value = number;
        }

        public static bool isPhoneNumber(string number)
        {
            return Regex.IsMatch(number, @"^[0]{ 1}[7]{1}[0-9]{8}$");
        }
    }
}
