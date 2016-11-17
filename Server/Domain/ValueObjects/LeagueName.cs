using System.Text.RegularExpressions;

namespace knatteligan.Domain.ValueObjects
{
    public class LeagueName
    {
        public string Value { get; set; }

        public LeagueName() { }

        public LeagueName(string name)
        {
            if (!IsLeagueName(name))
                throw new InvalidTeamNameException("Bad name.");

            Value = name;
        }

        private static bool IsLeagueName(string name)
        {
            return Regex.IsMatch(name, @"^[1-9a-öA-Ö\s]+$");
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
