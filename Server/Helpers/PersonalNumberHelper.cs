using System;
using System.Text.RegularExpressions;

namespace knatteligan.Helpers
{
    public class PersonalNumberHelper
    {
        public static PersonNumberType GetPersonalTypeForString(string str)
        {
            //19961107-1136
            //199611071136

            //961107-1136
            //9611071136
            var dash10 = new Regex(@"^\d{6}-\d{4}$");
            var noDash10 = new Regex(@"^\d{6}\d{4}$");

            var dash12 = new Regex(@"^\d{8}-\d{4}$");
            var noDash12 = new Regex(@"^\d{8}\d{4}$");

            if (dash10.IsMatch(str)) return PersonNumberType.Dash10;
            if (noDash10.IsMatch(str)) return PersonNumberType.NoDash10;
            if (dash12.IsMatch(str)) return PersonNumberType.Dash12;
            if (noDash12.IsMatch(str)) return PersonNumberType.NoDash12;


            return PersonNumberType.InvalidSyntax;
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