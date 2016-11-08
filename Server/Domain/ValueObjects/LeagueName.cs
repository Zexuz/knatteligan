using System.Text.RegularExpressions;

namespace knatteligan.Domain.ValueObjects
{
    public class LeagueName
    {
        public string Value { get;  set; }

        public LeagueName(string name)
        {
            if (!IsLeagueName(name))
                throw new InvalidTeamNameException("Bad name.");

            Value = name;
        }

        public LeagueName()
        {
            
        }

        //TODO: Better regex.
        private static bool IsLeagueName(string name)
        {
            return Regex.IsMatch(name, @"[a-öA-Ö ]+");
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
