using System;
using System.Text.RegularExpressions;

namespace knatteligan.Domain.ValueObjects
{
    public class LeagueName
    {
        public string Value { get; private set; }

        public LeagueName(string name)
        {
            if (!IsLeagueName(name))
                throw new Exception("Bad name.");

            Value = name;
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
