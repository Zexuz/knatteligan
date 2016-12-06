using System;
using knatteligan.Domain.ValueObjects;

namespace knatteligan.Helpers
{
    public static class ConvertHelper
    {
        public static PersonalNumber ConvertStringToPersonalNumber(string str)
        {
            if (str == string.Empty) return null;
            var type = PersonalNumberHelper.GetPersonalTypeForString(str);
            return PersonalNumberHelper.GetPersonalNumberFromStringWhenTypeIsKnowned(str, type);
        }
    }
}