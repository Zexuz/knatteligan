using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using knatteligan.Domain.ValueObjects;

namespace KnatteliganWPF.ValidationRules
{
    public class PhoneNumberValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {

            try
            {
                new PhoneNumber(value.ToString());
                return new ValidationResult(true, "");
            }
            catch (Exception)
            {
                return new ValidationResult(false, "Not a valid phone number.");
            }


        }
    }
}
