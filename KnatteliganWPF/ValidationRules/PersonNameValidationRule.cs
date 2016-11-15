using System;
using System.Globalization;
using System.Windows.Controls;
using knatteligan.Domain.ValueObjects;

namespace KnatteliganWPF.ValidationRules
{
    public class PersonNameValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                new PersonName(value.ToString());
                return new ValidationResult(true, "");
            }
            catch (Exception)
            {
                return new ValidationResult(false, "Not a valid name.");
            }
        }
    }
}
