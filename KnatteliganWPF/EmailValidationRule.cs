using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using knatteligan.Domain.ValueObjects;

namespace KnatteliganWPF
{
    public class EmailValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (Email.IsEmail(value.ToString()))
            {
                return new ValidationResult(true, "");
            }
            else
            {
                return new ValidationResult(false, "Not a valid email address");
            }
        }
    }
}
