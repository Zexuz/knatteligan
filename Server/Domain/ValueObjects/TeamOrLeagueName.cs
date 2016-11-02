using System;
using System.Text.RegularExpressions;

namespace knatteligan.Domain.ValueObjects
{
    public class TeamOrLeagueName
    {
        public string Value { get; private set; }

        public TeamOrLeagueName(string name)
        {
            if (!IsTeamOrLeagueName(name))
                throw new Exception("Bad name.");

            Value = name;
        }

        //TODO: Better regex.
        private static bool IsTeamOrLeagueName(string name)
        {
            return Regex.IsMatch(name, @"[a-öA-Ö ]+");
        }

    }
}
