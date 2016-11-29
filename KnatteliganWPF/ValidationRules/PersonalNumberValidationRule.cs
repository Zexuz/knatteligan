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


            var type = PersonalNumberHelper.GetPersonalTypeForString(str);

            if(type == PersonNumberType.InvalidSyntax)
                return new ValidationResult(false, "Valid format: YYMMDD-XXXX");

            try
            {
                PersonalNumberHelper.GetPersonalNumberFromStringWhenTypeIsKnowned(str, type);
            }
            catch (Exception)
            {
                return new ValidationResult(false, "Not a valid social number");
            }
            return new ValidationResult(true, "");
        }
    }
}
