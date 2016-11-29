using knatteligan.Domain.ValueObjects;
using System;
using System.Globalization;
using System.Windows.Controls;
using knatteligan.Helpers;

namespace KnatteliganWPF.ValidationRules
{
    class PersonalNumberValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var str = value.ToString();


            var type=  PersonalNumberHelper.GetPersonalTypeForString(str);

            if(type == PersonNumberType.InvalidSyntax)
                return new ValidationResult(false, "Valid format: YYMMDD-XXXX");

            return new ValidationResult(true, "");
        }
    }
}
