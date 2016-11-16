using System.Text.RegularExpressions;

namespace knatteligan.Domain.ValueObjects
{
    public class TeamName
    {
        public string Value { get; set; }

        public TeamName() { }

        public TeamName(string name)
        {
            if (!IsTeamName(name))
                throw new InvalidTeamNameException("Bad name.");

            Value = name;
        }
        //Todo fix regex so that user has to enter at least two chars
        private static bool IsTeamName(string name)
        {
            return Regex.IsMatch(name, @"^[1-9a-öA-Ö\s]+$");
        }

        public override string ToString()
        {
            return Value;
        }
    }
}