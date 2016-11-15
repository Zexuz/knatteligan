using System.Text.RegularExpressions;

namespace knatteligan.Domain.ValueObjects
{
    public class PhoneNumber
    {
        public string Value { get; set; }

        public PhoneNumber() { }

        public PhoneNumber(string number)
        {
            if (!IsPhoneNumber(number))
                throw new InvalidPhoneNumberException("Bad phone number");

            Value = number;
        }

        public static bool IsPhoneNumber(string number)
        {
            return Regex.IsMatch(number, @"^[0]{1}[7]{1}[0-9]{8}$");
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
