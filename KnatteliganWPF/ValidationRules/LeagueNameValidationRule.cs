using System;
using System.Globalization;
using System.Windows.Controls;
using knatteligan.Domain.ValueObjects;

namespace KnatteliganWPF.ValidationRules
{
    class LeagueNameValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                new LeagueName(value.ToString());
                return new ValidationResult(true, "");
            }
            catch (Exception)
            {
                return new ValidationResult(false, "2-30 characters & no symbols");
            }
        }
    }
}
