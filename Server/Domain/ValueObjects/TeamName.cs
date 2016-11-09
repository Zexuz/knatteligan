using System.Text.RegularExpressions;

namespace knatteligan.Domain.ValueObjects
{
    public class TeamName
    {
        public string Value { get; set; }

        public TeamName()
        {

        }

        public TeamName(string name)
        {
            if (!IsTeamName(name))
                throw new InvalidTeamNameException("Bad name.");

            Value = name;
        }

        //TODO: Better regex.
        private static bool IsTeamName(string name)
        {
            return Regex.IsMatch(name, @"[a-öA-Ö ]+");
        }

        public override string ToString()
        {
            return Value;
        }

    }
}