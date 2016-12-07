using System;
using System.Text.RegularExpressions;
using knatteligan.Domain.ValueObjects;

namespace knatteligan.Helpers
{
    public static class PersonalNumberHelper
    {
        private const string Dash10RegExString = @"^(\d{2})(\d{2})(\d{2})-(\d{4})$";
        private const string NoDash10RegExString = @"^(\d{2})(\d{2})(\d{2})(\d{4})$";
        private const string Dash12RegExString = @"^(\d{4})(\d{2})(\d{2})-(\d{4})$";
        private const string NoDash12RegExString = @"^(\d{4})(\d{2})(\d{2})(\d{4})$";


        public static PersonNumberType GetPersonalTypeForString(string str)
        {
            var dash10 = new Regex(Dash10RegExString);
            var noDash10 = new Regex(NoDash10RegExString);

            var dash12 = new Regex(Dash12RegExString);
            var noDash12 = new Regex(NoDash12RegExString);

            if (dash10.IsMatch(str)) return PersonNumberType.Dash10;
            if (noDash10.IsMatch(str)) return PersonNumberType.NoDash10;
            if (dash12.IsMatch(str)) return PersonNumberType.Dash12;
            if (noDash12.IsMatch(str)) return PersonNumberType.NoDash12;


            return PersonNumberType.InvalidSyntax;
        }

        public static PersonalNumber GetPersonalNumberFromStringWhenTypeIsKnowned(string str, PersonNumberType type)
        {
            if(type == PersonNumberType.InvalidSyntax) throw new Exception("invalid type state");
            switch (type)
            {
                case PersonNumberType.Dash12:
                    return GetNumberFromRegEx(new Regex(Dash12RegExString), str,false);
                case PersonNumberType.NoDash12:
                    return GetNumberFromRegEx(new Regex(NoDash12RegExString), str,false);
                case PersonNumberType.Dash10:
                    return GetNumberFromRegEx(new Regex(Dash10RegExString), str, true);
                case PersonNumberType.NoDash10:
                    return GetNumberFromRegEx(new Regex(NoDash10RegExString), str,true);
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        private static PersonalNumber GetNumberFromRegEx(Regex regEx, string str, bool isTenChar)
        {
            var match = regEx.Match(str);
            var year = Convert.ToInt32(match.Groups[1].Value);
            var month = Convert.ToInt32(match.Groups[2].Value);
            var day = Convert.ToInt32(match.Groups[3].Value);

            if (isTenChar)
                year += year > DateTime.Now.Year - 2000 ? 1900 : 2000;

            var dateTime = new DateTime(year, month, day);
            var last4 = match.Groups[4].Value;
            return new PersonalNumber(dateTime, last4);
        }
    }

    public enum PersonNumberType
    {
        Dash12,
        NoDash12,
        Dash10,
        NoDash10,
        InvalidSyntax
    }
}