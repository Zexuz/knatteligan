using System;
using knatteligan.Domain.ValueObjects;

namespace knatteligan.Helpers
{
    public static class ConvertHelper
    {

        //TODO: Is a helper the best idea here?
        public static PersonalNumber ConvertStringToPersonalNumber(string str)
        {
            if (str == string.Empty) return null;

            var ayear = int.Parse(str.Substring(0, 2));
            var month = int.Parse(str.Substring(2, 2));
            var day = int.Parse(str.Substring(4, 2));
            var last4 = str.Substring(7, 4);

            var date = new DateTime(ayear, month, day);

            return new PersonalNumber(date, last4);
        }
    }
}
