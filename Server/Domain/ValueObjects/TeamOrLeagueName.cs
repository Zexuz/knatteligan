using System;
using System.Text.RegularExpressions;

namespace knatteligan.Domain.ValueObjects
{
    public class TeamOrLeagueName
    {
        public string Value { get; private set; }

        public TeamOrLeagueName(string name)
        {
            if (IsLeagueName(name))
            {
                Value = name;
            }
            //TODO: Better exception.
            else
            {
                throw new Exception("Bad name.");
            }
        }

        //TODO: Better regex.
        private static bool IsLeagueName(string name)
        {
            return Regex.IsMatch(name, @"[a-öA-Ö ]+");
        }

    }
}
