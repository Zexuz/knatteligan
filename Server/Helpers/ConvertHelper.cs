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
            var type = PersonalNumberHelper.GetPersonalTypeForString(str);
            return PersonalNumberHelper.GetPersonalNumberFromStringWhenTypeIsKnowned(str, type);
        }
    }
}